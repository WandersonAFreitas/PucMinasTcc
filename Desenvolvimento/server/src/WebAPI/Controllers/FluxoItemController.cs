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
using System;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class FluxoItemController : BaseServiceController<long, FluxoItem>
    {
        private readonly IFluxoItemCheckListService _fluxoItemCheckListService;
        private readonly IFluxoItemTipoAnexoService _fluxoItemTipoAnexoService;
        private readonly IFluxoItemSetorService _fluxoItemSetorService;
        private readonly IFluxoSituacaoService _fluxoSituacaoService;
        private readonly IParametroService _parametroService;

        public FluxoItemController(
            IFluxoItemService service,
            IFluxoItemCheckListService fluxoItemCheckListService,
            IFluxoItemTipoAnexoService fluxoItemTipoAnexoService,
            IFluxoItemSetorService fluxoItemSetorService,
            IFluxoSituacaoService fluxoSituacaoService,
            IParametroService parametroService)
            : base(service)
        {
            _fluxoItemCheckListService = fluxoItemCheckListService;
            _fluxoItemTipoAnexoService = fluxoItemTipoAnexoService;
            _fluxoItemSetorService = fluxoItemSetorService;
            _fluxoSituacaoService = fluxoSituacaoService;
            _parametroService = parametroService;
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var fluxoItem = await query
                .Include(x => x.Fluxo)
                .Include(x => x.SituacaoAtual)
                .Include(x => x.Acao)
                .Include(x => x.ProximaSituacao)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(fluxoItem);
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();

            var rows = query
                .Include(x => x.Fluxo)
                .Include(x => x.SituacaoAtual)
                .Include(x => x.Acao)
                .Include(x => x.ProximaSituacao)
                .Where(postGrid, out int totalItens);

            var result = new Pagination<FluxoItem>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }


        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("todos/fluxo/{fluxoId}/acao/{acaoId}")]
        public async Task<IActionResult> GetSetoresPorFluxo(long fluxoId, long acaoId)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Where(x => x.FluxoId == fluxoId && x.AcaoId == acaoId)
                .Include(x => x.FluxoItemChecklists)
                .ThenInclude(x => x.TramiteChecklists)
                .Include(x => x.FluxoItemTiposAnexo)
                .ThenInclude(x => x.FluxoTipoAnexo)
                .OrderBy(x => x.Id);

            return Ok(rows);
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("todos/fluxo/{fluxoId}/situacao/{situacaoId}")]
        public async Task<IActionResult> GetFluxoItensPorFluxoEsituacao(long fluxoId, long situacaoId)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Where(x => x.FluxoId == fluxoId && x.SituacaoAtualId == situacaoId)
                .Include(x => x.FluxoItemChecklists)
                .ThenInclude(x => x.TramiteChecklists)
                .OrderBy(x => x.Id);

            return Ok(rows);
        }

        #region CheckList
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("checklist/find")]
        public async Task<IActionResult> FindChecklist([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoItemCheckListService.GetQueryableAsync();

            var rows = query.Include(x => x.FluxoItem)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoItemChecklist>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("checklist")]
        public async Task<IActionResult> PostChecklist([FromBody] FluxoItemChecklist fluxoItemChecklist)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newFluxoItemChecklist = new FluxoItemChecklist()
            {
                Nome = fluxoItemChecklist.Nome,
                FluxoItemId = fluxoItemChecklist.FluxoItemId
            };

            return Ok(await _fluxoItemCheckListService.AddAsync(newFluxoItemChecklist));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("checklist")]
        public async Task<IActionResult> PutChecklist([FromBody] FluxoItemChecklist fluxoItemChecklist)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoItemCheckListService.GetQueryableAsync();
            var _fluxoItemChecklist = query.Where(x => x.Id == fluxoItemChecklist.Id).FirstOrDefault();

            _fluxoItemChecklist.Nome = fluxoItemChecklist.Nome;
            await _fluxoItemCheckListService.UpdateAsync(_fluxoItemChecklist);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("checklist/{id}")]
        public async Task<IActionResult> DeleteChecklist(long id)
        {
            var query = await _fluxoItemCheckListService.GetQueryableAsync();
            var fluxoSituacao = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoItemCheckListService.DeleteAsync(fluxoSituacao);

            return Ok();
        }
        #endregion

        #region Anexo
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("anexo/find")]
        public async Task<IActionResult> FindAnexo([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoItemTipoAnexoService.GetQueryableAsync();

            var rows = query
                .Include(x => x.FluxoItem)
                .Include(x => x.FluxoTipoAnexo)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoItemTipoAnexo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("anexo")]
        public async Task<IActionResult> PostAnexo([FromBody] FluxoItemTipoAnexo fluxoItemTipoAnexo)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newFluxoItemTipoAnexo = new FluxoItemTipoAnexo()
            {
                Obrigatorio = fluxoItemTipoAnexo.Obrigatorio,
                ExigeAssinaturaDigital = fluxoItemTipoAnexo.ExigeAssinaturaDigital,
                FluxoTipoAnexoId = fluxoItemTipoAnexo.FluxoTipoAnexoId,
                FluxoItemId = fluxoItemTipoAnexo.FluxoItemId
            };

            return Ok(await _fluxoItemTipoAnexoService.AddAsync(newFluxoItemTipoAnexo));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("anexo")]
        public async Task<IActionResult> PutAnexo([FromBody] FluxoItemTipoAnexo fluxoItemTipoAnexo)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoItemTipoAnexoService.GetQueryableAsync();
            var _fluxoItemTipoAnexo = query.Where(x => x.Id == fluxoItemTipoAnexo.Id).FirstOrDefault();

            _fluxoItemTipoAnexo.Obrigatorio = fluxoItemTipoAnexo.Obrigatorio;
            _fluxoItemTipoAnexo.ExigeAssinaturaDigital = fluxoItemTipoAnexo.ExigeAssinaturaDigital;
            _fluxoItemTipoAnexo.FluxoTipoAnexoId = fluxoItemTipoAnexo.FluxoTipoAnexoId;
            await _fluxoItemTipoAnexoService.UpdateAsync(_fluxoItemTipoAnexo);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("anexo/{id}")]
        public async Task<IActionResult> DeleteAnexo(long id)
        {
            var query = await _fluxoItemTipoAnexoService.GetQueryableAsync();
            var fluxoItemTipoAnexo = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoItemTipoAnexoService.DeleteAsync(fluxoItemTipoAnexo);

            return Ok();
        }
        #endregion

        #region Setor
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("setor/find")]
        public async Task<IActionResult> FindSetor([FromBody] GridSettings postGrid)
        {
            var query = await _fluxoItemSetorService.GetQueryableAsync();

            var rows = query
                .Include(x => x.FluxoItem)
                .Include(x => x.Setor)
                .Where(postGrid, out int totalItens)
                .OrderBy(x => x.Id);

            var result = new Pagination<FluxoItemSetor>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);

        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpGet("setor/item-setores/fluxo/{id}")]
        public async Task<IActionResult> GetSetoresPorFluxo(long id)
        {
            var query = await _fluxoItemSetorService.GetQueryableAsync();
            var rows = query
                .Include(x => x.FluxoItem)
                .ThenInclude(x => x.FluxoItemChecklists)
                .ThenInclude(x => x.TramiteChecklists)
                .Include(x => x.Setor)
                .Where(x => x.FluxoItem.FluxoId == id)
                .OrderBy(x => x.Id);

            return Ok(rows);
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("setor")]
        public async Task<IActionResult> PostSetor([FromBody] FluxoItemSetor fluxoItemSetor)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var newFluxoItemSetor = new FluxoItemSetor()
            {
                SetorId = fluxoItemSetor.SetorId,
                FluxoItemId = fluxoItemSetor.FluxoItemId
            };

            return Ok(await _fluxoItemSetorService.AddAsync(newFluxoItemSetor));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPut("setor")]
        public async Task<IActionResult> PutSetor([FromBody] FluxoItemSetor fluxoItemSetor)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var query = await _fluxoItemSetorService.GetQueryableAsync();
            var _fluxoItemTipoAnexo = query.Where(x => x.Id == fluxoItemSetor.Id).FirstOrDefault();

            _fluxoItemTipoAnexo.SetorId = fluxoItemSetor.SetorId;
            await _fluxoItemSetorService.UpdateAsync(_fluxoItemTipoAnexo);

            return Ok();
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpDelete("setor/{id}")]
        public async Task<IActionResult> DeleteSetor(long id)
        {
            var query = await _fluxoItemSetorService.GetQueryableAsync();
            var _fluxoItemTipoAnexo = query.Where(x => x.Id == id).FirstOrDefault();

            await _fluxoItemSetorService.DeleteAsync(_fluxoItemTipoAnexo);

            return Ok();
        }
        #endregion
    }
}