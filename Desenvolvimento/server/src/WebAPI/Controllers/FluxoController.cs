using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using ApplicationCore.Helpers.Pagination;
using System.Linq;
using ApplicationCore.Extensions;
using WebAPI.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using WebAPI.ViewModels;
using ApplicationCore.Enums;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class FluxoController : BaseServiceController<long, Fluxo>
    {
        private readonly ISituacaoService _situacaoService;
        private readonly IFluxoSituacaoService _fluxoSituacaoService;
        private readonly IFluxoAcaoService _fluxoAcaoService;
        private readonly IFluxoTipoAnexoService _fluxoTipoAnexoService;
        private readonly IFluxoItemService _fluxoItemService;

        public FluxoController(
            IFluxoService service,
            ISituacaoService situacaoService,
            IFluxoSituacaoService fluxoSituacaoService,
            IFluxoAcaoService fluxoAcaoService,
            IFluxoTipoAnexoService fluxoTipoAnexoService,
            IFluxoItemService fluxoItemService)
            : base(service)
        {
            _situacaoService = situacaoService;
            _fluxoSituacaoService = fluxoSituacaoService;
            _fluxoAcaoService = fluxoAcaoService;
            _fluxoTipoAnexoService = fluxoTipoAnexoService;
            _fluxoItemService = fluxoItemService;
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var fluxo = await query
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(fluxo);
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();

            var rows = query
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<Fluxo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Post([FromBody] Fluxo tag)
        {
            var query = await _situacaoService.GetQueryableAsync();

            foreach (var item in query.Where(x => x.Padrao == true))
            {
                var _fluxoSituacao = new FluxoSituacao()
                {
                    Nome = item.Nome,
                    Padrao = item.Padrao,
                    TipoSituacao = item.TipoSituacao,
                    Fluxo = tag
                };

                tag.Situacoes.Add(_fluxoSituacao);
            }

            return Ok(await _service.AddAsync(tag));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("copiafluxo/{id}")]
        public async Task<IActionResult> CopiaFluxo(long id)
        {
            return await Task.Run(() =>
            {
                var query = _service.GetQueryableAsync().Result;
                var find = query.Where(x => x.Id == id).FirstOrDefault();

                #region Fluxo
                var newFluxo = new Fluxo
                {
                    Descricao = string.Concat(find.Descricao, "_Copia"),
                    Observacao = find.Observacao,
                    TramitarEm = find.TramitarEm
                };
                #endregion

                #region Situacao
                var queryFluxoSituacao = _fluxoSituacaoService.GetQueryableAsync().Result.ToList();
                var fluxoSituacoes = queryFluxoSituacao
                        .Where(x => x.FluxoId == id)
                        .OrderBy(x => x.Id);

                foreach (var item in fluxoSituacoes)
                {
                    var newFluxoSituacao = new FluxoSituacao
                    {
                        Fluxo = newFluxo,
                        Nome = item.Nome,
                        Padrao = item.Padrao,
                        TipoSituacao = item.TipoSituacao
                    };

                    newFluxo.Situacoes.Add(newFluxoSituacao);
                }
                #endregion

                #region Ação
                var queryFluxoAcao = _fluxoAcaoService.GetQueryableAsync().Result.ToList();
                var fluxoAcoes = queryFluxoAcao
                        .Where(x => x.FluxoId == id)
                        .OrderBy(x => x.Id);

                foreach (var item in fluxoAcoes)
                {
                    var newFluxoAcao = new FluxoAcao
                    {
                        Fluxo = newFluxo,
                        Nome = item.Nome
                    };
                    
                    newFluxo.Acoes.Add(newFluxoAcao);
                }
                #endregion

                #region Tipo de Anexo
                var queryFluxoTipoAnexo = _fluxoTipoAnexoService.GetQueryableAsync().Result.ToList();
                var fluxoTiposAnexo = queryFluxoTipoAnexo
                        .Where(x => x.FluxoId == id)
                        .OrderBy(x => x.Id);

                foreach (var item in fluxoTiposAnexo)
                {
                    var newFluxoTipoAnexo = new FluxoTipoAnexo
                    {
                        Fluxo = newFluxo,
                        Nome = item.Nome
                    };

                    newFluxo.TiposAnexo.Add(newFluxoTipoAnexo);
                }
                #endregion

                #region Fluxo item
                var queryFluxoItem = _fluxoItemService.GetQueryableAsync().Result;
                var fluxoItems = queryFluxoItem
                        .Include(x => x.FluxoItemSetores).ThenInclude(x => x.Setor)
                        .Include(x => x.FluxoItemTiposAnexo).ThenInclude(x => x.FluxoTipoAnexo)
                        .Include(x => x.FluxoItemChecklists)
                        .Where(x => x.FluxoId == id)
                        .OrderBy(x => x.Id);

                foreach (var fluxoItem in fluxoItems)
                {
                    var situacaoAtual = queryFluxoSituacao.Where(x => x.Id == fluxoItem.SituacaoAtualId).FirstOrDefault();
                    situacaoAtual = newFluxo.Situacoes.Where(x => x.Nome == situacaoAtual.Nome).FirstOrDefault();

                    var acao = queryFluxoAcao.Where(x => x.Id == fluxoItem.AcaoId).FirstOrDefault();
                    acao = newFluxo.Acoes.Where(x => x.Nome == acao.Nome).FirstOrDefault();

                    var proximaSituacao = queryFluxoSituacao.Where(x => x.Id == fluxoItem.ProximaSituacaoId).FirstOrDefault();
                    proximaSituacao = newFluxo.Situacoes.Where(x => x.Nome == proximaSituacao.Nome).FirstOrDefault();

                    var newFluxoItem = new FluxoItem()
                    {
                        Fluxo = newFluxo,
                        SituacaoAtual = situacaoAtual,
                        Acao = acao,
                        ProximaSituacao = proximaSituacao
                    };

                    #region Setores
                    foreach (var fluxoItemSetor in fluxoItem.FluxoItemSetores)
                    {
                        var newfluxoItemSetor = new FluxoItemSetor
                        {
                            SetorId = fluxoItemSetor.Setor.Id,
                            FluxoItem = newFluxoItem
                        };

                        newFluxoItem.FluxoItemSetores.Add(newfluxoItemSetor);
                    }
                    #endregion

                    #region CheckList
                    foreach (var fluxoItemChecklist in fluxoItem.FluxoItemChecklists)
                    {
                        var newfluxoItemChecklist = new FluxoItemChecklist
                        {
                            Nome = fluxoItemChecklist.Nome,
                            FluxoItem = newFluxoItem
                        };

                        newFluxoItem.FluxoItemChecklists.Add(newfluxoItemChecklist);
                    }
                    #endregion

                    #region CheckList
                    foreach (var fluxoItemTiposAnexo in fluxoItem.FluxoItemTiposAnexo)
                    {
                        var fluxoTipoAnexo = newFluxo.TiposAnexo
                                                .Where(x => x.Nome == fluxoItemTiposAnexo.FluxoTipoAnexo.Nome)
                                                .FirstOrDefault();

                        var newfluxoItemTipoAnexo = new FluxoItemTipoAnexo
                        {
                            FluxoTipoAnexo = fluxoTipoAnexo,
                            ExigeAssinaturaDigital = fluxoItemTiposAnexo.ExigeAssinaturaDigital,
                            Obrigatorio = fluxoItemTiposAnexo.Obrigatorio,
                            FluxoItem = newFluxoItem
                        };

                        newFluxoItem.FluxoItemTiposAnexo.Add(newfluxoItemTipoAnexo);
                    }
                    #endregion

                    newFluxo.FluxoItems.Add(newFluxoItem);
                }
                #endregion

                return Ok(_service.AddAsync(newFluxo));
            });
        }

        #region Situação
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("situacao/find")]
        public async Task<IActionResult> FindSituacao([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoSituacaoService.GetQueryableAsync();

            var rows = query.Include(x => x.Fluxo)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoSituacao>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("situacao")]
        public async Task<IActionResult> PostSituacao([FromBody] FluxoSituacao fluxoSituacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());
            
            var newFluxoSituacao = new FluxoSituacao()
            {
                Nome = fluxoSituacao.Nome,
                FluxoId = fluxoSituacao.FluxoId,
                TipoSituacao = (int)TipoSituacaoEnum.Todas
            };

            return Ok(await _fluxoSituacaoService.AddAsync(newFluxoSituacao));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("situacao")]
        public async Task<IActionResult> PutSituacao([FromBody] FluxoSituacao fluxoSituacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoSituacaoService.GetQueryableAsync();
            var _fluxoSituacao = query.Where(x => x.Id == fluxoSituacao.Id).FirstOrDefault();

            _fluxoSituacao.Nome = fluxoSituacao.Nome;
            await _fluxoSituacaoService.UpdateAsync(_fluxoSituacao);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("situacao/{id}")]
        public async Task<IActionResult> DeleteSituacao(long id)
        {
            var query = await _fluxoSituacaoService.GetQueryableAsync();
            var fluxoSituacao = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoSituacaoService.DeleteAsync(fluxoSituacao);

            return Ok();
        }
        #endregion

        #region Ação
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("acao/find")]
        public async Task<IActionResult> FindAcao([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoAcaoService.GetQueryableAsync();

            var rows = query.Include(x => x.Fluxo)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoAcao>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("acao")]
        public async Task<IActionResult> PostAcao([FromBody] FluxoAcao fluxoAcao)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newFluxoAcao = new FluxoAcao()
            {
                Nome = fluxoAcao.Nome,
                FluxoId = fluxoAcao.FluxoId
            };

            return Ok(await _fluxoAcaoService.AddAsync(newFluxoAcao));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("acao")]
        public async Task<IActionResult> PutAcao([FromBody] FluxoAcao fluxoAcao)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoAcaoService.GetQueryableAsync();
            var _fluxoAcao = query.Where(x => x.Id == fluxoAcao.Id).FirstOrDefault();

            _fluxoAcao.Nome = fluxoAcao.Nome;
            await _fluxoAcaoService.UpdateAsync(_fluxoAcao);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("acao/{id}")]
        public async Task<IActionResult> DeleteAcao(long id)
        {
            var query = await _fluxoAcaoService.GetQueryableAsync();
            var fluxoAcao = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoAcaoService.DeleteAsync(fluxoAcao);

            return Ok();
        }
        #endregion

        #region Tipo Anexo
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("tipoanexo/find")]
        public async Task<IActionResult> FindTipoAnexo([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoTipoAnexoService.GetQueryableAsync();

            var rows = query.Include(x => x.Fluxo)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoTipoAnexo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("tipoanexo")]
        public async Task<IActionResult> PostTipoAnexo([FromBody] FluxoTipoAnexo fluxoTipoAnexo)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newFluxoTipoAnexo = new FluxoTipoAnexo()
            {
                Nome = fluxoTipoAnexo.Nome,
                FluxoId = fluxoTipoAnexo.FluxoId
            };

            return Ok(await _fluxoTipoAnexoService.AddAsync(newFluxoTipoAnexo));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("tipoanexo")]
        public async Task<IActionResult> PutTipoAnexo([FromBody] FluxoTipoAnexo fluxoTipoAnexo)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoTipoAnexoService.GetQueryableAsync();
            var _fluxoTipoAnexo = query.Where(x => x.Id == fluxoTipoAnexo.Id).FirstOrDefault();

            _fluxoTipoAnexo.Nome = fluxoTipoAnexo.Nome;
            await _fluxoTipoAnexoService.UpdateAsync(_fluxoTipoAnexo);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("tipoanexo/{id}")]
        public async Task<IActionResult> DeleteTipoAnexo(long id)
        {
            var query = await _fluxoTipoAnexoService.GetQueryableAsync();
            var fluxoTipoAnexo = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoTipoAnexoService.DeleteAsync(fluxoTipoAnexo);

            return Ok();
        }
        #endregion
    }
}