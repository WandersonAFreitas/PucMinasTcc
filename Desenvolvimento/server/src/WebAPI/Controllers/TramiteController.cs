using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Helpers.Pagination;
using ApplicationCore.Interfaces.Services;
using HashidsNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.ViewModels.Hashes;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TramiteController : BaseServiceController<long, Tramite>
    {

        private readonly IEmailService _emailService;
        private readonly IProcessoService _processoService;
        private readonly IFluxoAcaoService _fluxoAcaoService;
        private readonly IParametroService _parametroService;

        public TramiteController(
            ITramiteService service,
            IProcessoService processoService,
            IFluxoAcaoService fluxoAcaoService,
            IEmailService emailService,
            IParametroService parametroService
            )
            : base(service)
        {
            _processoService = processoService;
            _fluxoAcaoService = fluxoAcaoService;
            _emailService = emailService;
            _parametroService = parametroService;
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Setor)
                .Include(x => x.Situacao)
                .Include(x => x.SituacaoDoProcessoNoTramite)
                .Include(x => x.Acao)
                .ThenInclude(x => x.Fluxo)
                .ThenInclude(x => x.FluxoItems)
                .Include(x => x.Responsavel)
                .Include(x => x.TramiteChecklists)
                .Include(x => x.TramiteArquivos)
                .ThenInclude(x => x.FluxoItemTipoAnexo)
                .ThenInclude(x => x.FluxoTipoAnexo)
                .Include(x => x.TramiteArquivos)
                .ThenInclude(x => x.Arquivo)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Tramite>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var processo = await query
                .Include(x => x.Setor)
                .Include(x => x.Situacao)
                .Include(x => x.SituacaoDoProcessoNoTramite)
                .Include(x => x.Acao)
                .ThenInclude(x => x.Fluxo)
                .ThenInclude(x => x.FluxoItems)
                .Include(x => x.Responsavel)
                .Include(x => x.TramiteChecklists)
                .Include(x => x.TramiteArquivos)
                .ThenInclude(x => x.FluxoItemTipoAnexo)
                .ThenInclude(x => x.FluxoTipoAnexo)
                .Include(x => x.TramiteArquivos)
                .ThenInclude(x => x.Arquivo)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(processo);
        }

        [HttpGet("nao-tramitado/processo/{idProcesso}")]
        public async Task<IActionResult> GetHaTramiteNaoTramitadoNoProcesso(long idProcesso)
        {
            var query = await _service.GetQueryableAsync();
            var haNaoTramitado = query.Where(x => x.ProcessoId == idProcesso).Any(x => !x.Tramitado);

            return Ok(haNaoTramitado);
        }

        [AllowAnonymous]
        [HttpGet("hash/{hash}/tramitar")]
        public async Task<IActionResult> GetTramitar(string hash)
        {
            var hashids = new Hashids("Teste de unique salt value");
            var tramiteHashHex = hashids.DecodeHex(hash);

            if (string.IsNullOrEmpty(tramiteHashHex))
                return Ok("Erro ao validar Hash");

            var tramiteHashString = string.Join("", Regex.Split(tramiteHashHex, "(?<=\\G..)(?!$)").Select(x => (char)Convert.ToByte(x, 16)));
            var tramiteHash = JsonConvert.DeserializeObject<TramiteHash>(tramiteHashString);

            var query = await _service.GetQueryableAsync();

            var tramite = await query.Where(x => x.Id == tramiteHash.TramiteId)
                .Include(x => x.Acao)
                .ThenInclude(x => x.Fluxo)
                .ThenInclude(x => x.FluxoItems)
                .Include(x => x.Processo)
                .ThenInclude(x => x.Responsavel)
                .Include(x => x.Processo)
                .ThenInclude(x => x.ProcessoAutores)
                .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync();

            if (tramite == null)
                return Ok("Tramite não encontrado");

            if (tramite.Tramitado)
                return Ok("Esse item já foi tramitado");

            if (tramite.ProcessoId != tramiteHash.ProcessoId || tramite.SituacaoId != tramiteHash.SituacaoId)
                return Ok("Falha ao tramitar");

            // TODO: Verificar necessidade de checagem de resposável do processo no tramite

            await Tramitar(tramite, false);

            return Ok("Tramitado");
        }

        [HttpGet("{id}/tramitar")]
        public async Task<IActionResult> GetTramitar(long id)
        {
            var query = await _service.GetQueryableAsync();
            var tramite = await query.Where(x => x.Id == id)
                .Include(x => x.Acao)
                .ThenInclude(x => x.Fluxo)
                .ThenInclude(x => x.FluxoItems)
                .Include(x => x.Processo)
                .ThenInclude(x => x.Responsavel)
                .Include(x => x.Processo)
                .ThenInclude(x => x.ProcessoAutores)
                .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync();

            return Ok(await Tramitar(tramite));
        }

        private async Task<Tramite> Tramitar(Tramite tramite, bool checharResponsavel = false)
        {
            tramite.Tramitado = true;
            tramite.DataTramite = DateTime.Now;

            var processo = await _processoService.GetByIdAsync(tramite.ProcessoId);

            if (checharResponsavel && processo.ResponsavelId != UserClaim().Id)
            {
                if (processo.ResponsavelId == null)
                    throw new ClientErrorException($"O Processo não pode ser tramitado, pois não há responsável.", StatusCodes.Status409Conflict);// TODO: Criar validator
                throw new ClientErrorException($"O Processo só poder ser tramitado por: {processo.Responsavel.UserName}", StatusCodes.Status409Conflict);// TODO: Criar validator
            }

            processo.SetorId = tramite.SetorId;
            processo.ResponsavelId = null;

            if (tramite.Acao != null && tramite.Acao.Fluxo != null && tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.FluxoDefinido && tramite.Acao.Fluxo.FluxoItems.Any())
            {
                processo.SituacaoId = tramite.Acao.Fluxo.FluxoItems.FirstOrDefault(x => x.AcaoId == tramite.Acao.Id).ProximaSituacaoId;
            }
            else if (tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.EntreSetoresDeTodasAsEmpresa ||
                tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.SetoresDaEmpresaDoProcesso)
            {
                processo.SituacaoId = tramite.SituacaoId;
            }

            await _processoService.UpdateAsync(processo);
            await _service.UpdateAsync(tramite);

            EnviarEmailParaAutoresQuandoTramitado(tramite);

            return tramite;
        }

        // TODO: REFATORAR POST E PUT
        public override async Task<IActionResult> Post([FromBody] Tramite addTramite)
        {
            var queryProcesso = await _processoService.GetQueryableAsync();
            var processo = await queryProcesso.Where(x => x.Id == addTramite.ProcessoId).Include(x => x.Responsavel).FirstOrDefaultAsync();

            if (addTramite.Tramitado)
            {
                addTramite.DataTramite = DateTime.Now;

                if (processo.ResponsavelId != UserClaim().Id)
                {
                    if (processo.ResponsavelId == null)
                        throw new ClientErrorException($"O Processo não pode ser tramitado, pois não há responsável.", StatusCodes.Status409Conflict);// TODO: Criar validator
                    throw new ClientErrorException($"O Processo só poder ser tramitado por: {processo.Responsavel.UserName}", StatusCodes.Status409Conflict);// TODO: Criar validator
                }
            }

            var situacaoAtualDoTramite = new FluxoSituacao();

            if (addTramite.SituacaoId == 0)
            {
                var queryAcao = await _fluxoAcaoService.GetQueryableAsync();
                var acao = await queryAcao.Where(x => x.Id == addTramite.AcaoId)
                    .Include(x => x.Fluxo)
                    .ThenInclude(x => x.FluxoItems)
                    .ThenInclude(x => x.ProximaSituacao)
                    .FirstOrDefaultAsync();


                if (acao != null && acao.Fluxo != null && acao.Fluxo.TramitarEm == (int)TramitarEmEnum.FluxoDefinido && acao.Fluxo.FluxoItems.Any())
                {
                    var fluxoItem = acao.Fluxo.FluxoItems.FirstOrDefault(x => x.AcaoId == acao.Id);
                    if (fluxoItem != null)
                    {
                        situacaoAtualDoTramite = fluxoItem.ProximaSituacao;
                        addTramite.SituacaoId = fluxoItem.ProximaSituacaoId;
                    }
                }
            }

            var newTramite = await _service.AddAsync(addTramite);

            var query = await _service.GetQueryableAsync();
            var tramite = await query.Where(x => x.Id == newTramite.Id)
                .Include(x => x.Situacao)
                .Include(x => x.Responsavel)
                .Include(x => x.Acao)
                .ThenInclude(x => x.Fluxo)
                .ThenInclude(x => x.FluxoItems)
                .Include(x => x.Processo)
                .ThenInclude(x => x.ProcessoAutores)
                .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync();

            if (newTramite.Tramitado)
            {
                processo.SetorId = tramite.SetorId;
                processo.ResponsavelId = null;

                if (tramite.Acao != null && tramite.Acao.Fluxo != null && tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.FluxoDefinido && tramite.Acao.Fluxo.FluxoItems.Any())
                {
                    processo.SituacaoId = tramite.Acao.Fluxo.FluxoItems.FirstOrDefault(x => x.AcaoId == tramite.Acao.Id).ProximaSituacaoId;
                }
                else if (tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.EntreSetoresDeTodasAsEmpresa ||
                tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.SetoresDaEmpresaDoProcesso)
                {
                    processo.SituacaoId = tramite.SituacaoId;
                }

                await _processoService.UpdateAsync(processo);

                EnviarEmailParaAutoresQuandoTramitado(tramite);
            }
            else if (situacaoAtualDoTramite.Id > 0 && situacaoAtualDoTramite.TipoSituacao == (int)TipoSituacaoEnum.SituacaoFinal)
            {
                EnviaEmailParaResponsavelDoProcessoQuandoTramiteEmSituacaoFinal(tramite);
            }

            return Ok(newTramite);
        }

        // TODO: REFATORAR POST E PUT
        public override async Task<IActionResult> Put([FromBody] Tramite updateTramite)
        {
            var queryProcesso = await _processoService.GetQueryableAsync();
            var processo = await queryProcesso.Where(x => x.Id == updateTramite.ProcessoId).Include(x => x.Responsavel).FirstOrDefaultAsync();

            if (updateTramite.Tramitado)
            {
                updateTramite.DataTramite = DateTime.Now;

                if (processo.ResponsavelId != UserClaim().Id)
                {
                    if (processo.ResponsavelId == null)
                        throw new ClientErrorException($"O Processo não pode ser tramitado, pois não há responsável.", StatusCodes.Status409Conflict);// TODO: Criar validator
                    throw new ClientErrorException($"O Processo só poder ser tramitado por: {processo.Responsavel.UserName}", StatusCodes.Status409Conflict);// TODO: Criar validator
                }
            }

            await _service.UpdateAsync(updateTramite);

            var query = await _service.GetQueryableAsync();
            var tramite = await query.Where(x => x.Id == updateTramite.Id)
                 .Include(x => x.Situacao)
                 .Include(x => x.Responsavel)
                 .Include(x => x.Acao)
                 .ThenInclude(x => x.Fluxo)
                 .ThenInclude(x => x.FluxoItems)
                 .Include(x => x.Processo)
                 .ThenInclude(x => x.ProcessoAutores)
                 .ThenInclude(x => x.Autor)
                 .FirstOrDefaultAsync();

            if (updateTramite.Tramitado)
            {
                processo.SetorId = tramite.SetorId;
                processo.ResponsavelId = null;

                if (tramite.Acao != null && tramite.Acao.Fluxo != null && tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.FluxoDefinido && tramite.Acao.Fluxo.FluxoItems.Any())
                {
                    processo.SituacaoId = tramite.Acao.Fluxo.FluxoItems.FirstOrDefault(x => x.AcaoId == tramite.Acao.Id).ProximaSituacaoId;
                }
                else if (tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.EntreSetoresDeTodasAsEmpresa ||
                tramite.Acao.Fluxo.TramitarEm == (int)TramitarEmEnum.SetoresDaEmpresaDoProcesso)
                {
                    processo.SituacaoId = tramite.SituacaoId;
                }

                await _processoService.UpdateAsync(processo);

                EnviarEmailParaAutoresQuandoTramitado(tramite);
            }
            else if (tramite.SituacaoId > 0 && tramite.Situacao.TipoSituacao == (int)TipoSituacaoEnum.SituacaoFinal)
            {
                EnviaEmailParaResponsavelDoProcessoQuandoTramiteEmSituacaoFinal(tramite);
            }


            return Ok();
        }

        private void EnviaEmailParaResponsavelDoProcessoQuandoTramiteEmSituacaoFinal(Tramite tramite)
        {
            var timeStamp = DateTime.Now.ToFileTime();
            var processoSequencialAno = $"{tramite.Processo.Sequencial}/{tramite.Processo.Ano}";
            var hashids = new Hashids("Teste de unique salt value");

            var tramiteHash = new TramiteHash
            {
                TramiteId = tramite.Id,
                ProcessoId = tramite.ProcessoId,
                SituacaoId = tramite.SituacaoId,
                TimeStamp = timeStamp
            };

            var tramiteHashString = JsonConvert.SerializeObject(tramiteHash);

            string tramiteHashHex = string.Concat(tramiteHashString.Select(x => ((int)x).ToString("x")));

            var hash = hashids.EncodeHex(tramiteHashHex);

            var _numeroProcesso = $"<a href=\"http://localhost:5000/api/tramite/hash/{hash}/tramitar\">{processoSequencialAno}</a>";
            var _assunto = $"SCA: Aguardando tramite no Processo nº {processoSequencialAno}!";
            var _mensagem = $@"O Processo nº {_numeroProcesso} aguadando tramite.";

            _mensagem = EmailTramiteProcesso(_assunto, _mensagem);
            
            _emailService.Send(_assunto, _mensagem, new string[] { tramite.Responsavel.Email });
        }

        private void EnviarEmailParaAutoresQuandoTramitado(Tramite tramite)
        {
            if (tramite.Tramitado && tramite.EnviarEmailAutores && tramite.Processo.ProcessoAutores != null && tramite.Processo.ProcessoAutores.Any(x => x.Autor != null))
            {
                var _assunto = $"SCA: Tramite no Processo nº {tramite.Processo.Sequencial}/{tramite.Processo.Ano}!";
                var _mensagem = $"O Processo nº {tramite.Processo.Sequencial}/{tramite.Processo.Ano} foi tramitado no dia: {DateTime.Now}";

                _mensagem = EmailTramiteProcesso(_assunto, _mensagem);

                var emails = tramite.Processo.ProcessoAutores.Select(x => x.Autor.Email).ToArray();
                _emailService.Send(_assunto, _mensagem, emails);
            }
        }

        private string EmailTramiteProcesso(string assunto, string mensagem)
        {
            var query = _parametroService.GetQueryableAsync().Result;
            var parametros = query.Where(x => x.Nome == ParametroEnum.EmailTramiteProcesso.Descricao()).FirstOrDefault();

            var _mensagem = parametros.Valor;
            _mensagem = _mensagem.Replace("@Assunto", assunto);
            _mensagem = _mensagem.Replace("@Mensagem", mensagem);

            return _mensagem;
        }
    }
}