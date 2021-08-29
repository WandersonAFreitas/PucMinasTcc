using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.IO.Compression;
using Newtonsoft.Json;
using WebAPI.ViewModels;
using System.Web;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ArquivoController : BaseServiceController<long, Arquivo>
    {

        private readonly IArquivoService _arquivoService;
        private readonly IAssuntoService _assuntoService;
        private readonly IAssuntoArquivoService _assuntoArquivoService;
        private readonly ITramiteArquivoService _tramiteArquivoService;
        private const string regexNomeArquivo = "[^A-Za-z0-9.]+";

        public ArquivoController(
            IArquivoService arquivoService,
            IAssuntoService assuntoService,
            IAssuntoArquivoService assuntoArquivoService,
            ITramiteArquivoService tramiteArquivoService
            )
            : base(arquivoService)
        {
            _arquivoService = arquivoService;
            _assuntoService = assuntoService;
            _assuntoArquivoService = assuntoArquivoService;
            _tramiteArquivoService = tramiteArquivoService;
        }

        // TODO: MELHORAR CRIANDO SERVICOS PARA CADA TIPO DE ARQUIVO
        [HttpPost("upload")]
        [HttpPost("upload/assunto/{assuntoId}")]
        public async Task<IActionResult> PostUploadFiles(long? assuntoId = null, long? tramiteId = null)
        {
            var files = Request.Form.Files;

            var arquivos = new List<Arquivo>();
            var hashs = new List<string>();

            foreach (var file in files)
            {
                if ((file.Length / 1048576) > 50)
                    throw new Exception("Não é permitido a inclusão de arquivo cujo tamanho seja maior que 50 MB. Obs.: Nenhum arquivo foi incluído");

                var nomeArquivo = file.FileName.Trim('\"');

                using (Stream stream = file.OpenReadStream())
                {
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)file.Length);
                        var arquivo = new Arquivo
                        {
                            Nome = nomeArquivo,
                            UserId = UserClaim().Id,
                            Bytes = fileContent
                        };
                        arquivos.Add(arquivo);
                    }
                }
            }

            foreach (var arquivo in arquivos)
            {
                var newArquivo = await _service.AddAsync(arquivo);
                hashs.Add(newArquivo.Hash);

                // TODO: MELHORAR CRIANDO SERVICOS PARA CADA TIPO DE ARQUIVO
                if (assuntoId != null && assuntoId > 0)
                {
                    var assuntoArquivo = new AssuntoArquivo
                    {
                        ArquivoId = newArquivo.Id,
                        AssuntoId = assuntoId.Value
                    };
                    await _assuntoArquivoService.AddAsync(assuntoArquivo);
                }

                // TODO: MELHORAR CRIANDO SERVICOS PARA CADA TIPO DE ARQUIVO
                if (tramiteId != null && tramiteId > 0)
                {
                    var tramiteArquivo = new TramiteArquivo
                    {
                        ArquivoId = newArquivo.Id,
                        TramiteId = tramiteId.Value
                    };
                    await _tramiteArquivoService.AddAsync(tramiteArquivo);
                }
            }

            return Ok(hashs);
        }

        // TODO: MELHORAR CRIANDO SERVICOS PARA TramiteArquivo
        [HttpPost("upload/tramite/{tramiteId}")]
        public async Task<IActionResult> PostUploadFilesTramiteArquivo(long tramiteId)
        {
            var files = Request.Form.Files;

            var arquivos = new List<FluxoItemTipoAnexoArquivoViewModel>();
            var hashs = new List<string>();

            foreach (var file in files)
            {
                if ((file.Length / 1048576) > 50)
                    throw new Exception("Não é permitido a inclusão de arquivo cujo tamanho seja maior que 50 MB. Obs.: Nenhum arquivo foi incluído");

                var nomeArquivo = file.FileName.Trim('\"');

                var objetoTratadoRegex = Regex.Replace(file.Name, "<.*?>", string.Empty);
                var objetoTratadoUrlDecode = HttpUtility.UrlDecode(objetoTratadoRegex);
                var tramiteArquivoViewModel = JsonConvert.DeserializeObject<TramiteArquivoViewModel>(objetoTratadoUrlDecode);

                using (Stream stream = file.OpenReadStream())
                {
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)file.Length);
                        var arquivo = new FluxoItemTipoAnexoArquivoViewModel
                        {
                            FluxoItemTipoAnexoId = tramiteArquivoViewModel.FluxoItemTipoAnexoId,
                            Arquivo = new Arquivo
                            {
                                Nome = nomeArquivo,
                                UserId = UserClaim().Id,
                                Bytes = fileContent
                            }
                        };

                        if (tramiteArquivoViewModel.ArquivoId.HasValue && tramiteArquivoViewModel.ArquivoId.Value > 0)
                        {
                            arquivo.TramiteArquivoId = tramiteArquivoViewModel.Id;
                            arquivo.Arquivo.Id = tramiteArquivoViewModel.ArquivoId.Value;
                        }

                        arquivos.Add(arquivo);
                    }
                }
            }

            foreach (var arquivo in arquivos)
            {
                var arquivoId = arquivo.Arquivo.Id;
                if (arquivoId > 0)
                {
                    await _service.UpdateAsync(arquivo.Arquivo);
                    hashs.Add(arquivo.Arquivo.Hash);

                    var tramiteArquivo = new TramiteArquivo
                    {
                        Id = arquivo.TramiteArquivoId.Value,
                        ArquivoId = arquivoId,
                        TramiteId = tramiteId,
                        FluxoItemTipoAnexoId = arquivo.FluxoItemTipoAnexoId
                    };

                    await _tramiteArquivoService.UpdateAsync(tramiteArquivo);
                }
                else
                {
                    var newArquivo = await _service.AddAsync(arquivo.Arquivo);
                    arquivoId = newArquivo.Id;
                    hashs.Add(newArquivo.Hash);

                    var tramiteArquivo = new TramiteArquivo
                    {
                        ArquivoId = arquivoId,
                        TramiteId = tramiteId,
                        FluxoItemTipoAnexoId = arquivo.FluxoItemTipoAnexoId
                    };

                    await _tramiteArquivoService.AddAsync(tramiteArquivo);
                }

            }

            return Ok(hashs);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string hash)
        {
            var arquivo = await _arquivoService.GetByHashAsync(hash);

            if (arquivo == null)
                throw new Exception("Arquivo Não encontrado");

            var arquivoNome = Regex.Replace(arquivo.Nome, regexNomeArquivo, "_");

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = WebUtility.HtmlEncode(arquivoNome),
                Inline = true
            };

            try
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            FileContentResult result = new FileContentResult(arquivo.Bytes, arquivo.Tipo)
            {
                FileDownloadName = arquivoNome
            };

            return result;
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] string[] hashes)
        {
            var arquivos = await _arquivoService.GetByHashesAsync(hashes);

            byte[] buffer = null;
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update, leaveOpen: true))
                {
                    foreach (Arquivo dataFile in arquivos)
                    {
                        if (dataFile.Bytes != null)
                        {
                            ZipArchiveEntry zipEntry = archive.CreateEntry(dataFile.Nome);

                            var conteudo = new MemoryStream(dataFile.Bytes);

                            conteudo.WriteTo(zipEntry.Open());
                        }
                    }
                }
                buffer = zipStream.ToArray();
            }

            return new FileContentResult(buffer, "application/zip") { FileDownloadName = "files.zip" };
        }
    }
}