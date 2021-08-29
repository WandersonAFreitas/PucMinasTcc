using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Helpers.Pagination;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProcessoController : BaseServiceController<long, Processo>
    {

        private readonly IUserService _userService;

        public ProcessoController(
            IProcessoService service,
            IUserService userService
            )
            : base(service)
        {
            _userService = userService;
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Empresa)
                .Include(x => x.Assunto)
                .Include(x => x.Situacao)
                .Include(x => x.Responsavel)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Processo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var processo = await query
                .Include(x => x.Empresa)
                .Include(x => x.Setor)
                .Include(x => x.Assunto)
                .ThenInclude(x => x.Fluxo)
                .Include(x => x.Situacao)
                .Include(x => x.Responsavel)
                .Include(x => x.Tramites)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(processo);
        }

        public override async Task<IActionResult> Post([FromBody] Processo addProcesso)
        {
            var currentDate = DateTime.Now;

            addProcesso.Ano = currentDate.Year;


            var query = await _service.GetQueryableAsync();
            var processo = query.Where(x => x.Ano == addProcesso.Ano).OrderByDescending(x => x.Sequencial).FirstOrDefault();

            if (processo != null)
            {
                addProcesso.Sequencial = processo.Sequencial + 1;
            }
            else
            {
                addProcesso.Sequencial = 1;
            }

            addProcesso.UltimaAltercao = currentDate;
            addProcesso.Criacao = currentDate;

            return Ok(await _service.AddAsync(addProcesso));
        }

        public override async Task<IActionResult> Put([FromBody] Processo updateProcesso)
        {
            updateProcesso.UltimaAltercao = DateTime.Now;
            await _service.UpdateAsync(updateProcesso);
            return Ok();
        }

        [HttpGet("{id}/tramite/valido")]
        public async Task<IActionResult> GetTramiteValidoDoProcesso(long id)
        {
            var query = await _service.GetQueryableAsync();
            var processo = await query
                .Include(x => x.Tramites)
                .ThenInclude(x => x.Acao)
                .Include(x => x.Tramites)
                .ThenInclude(x => x.Setor)
                .FirstOrDefaultAsync(x => x.Id == id);

            var tramite = processo.Tramites.FirstOrDefault(x => !x.Tramitado);

            return Ok(tramite);
        }

        [HttpPost("{processoId}/responsavel/{responsavelId}")]
        public async Task<IActionResult> SetResponsavel(long processoId, long responsavelId)
        {
            var processo = await _service.GetByIdAsync(processoId);
            processo.ResponsavelId = responsavelId;
            await _service.UpdateAsync(processo);

            return Ok(await _userService.GetByIdAsync(responsavelId));
        }

        [HttpGet("quantidades/responsavel/{responsavelId}")]
        public async Task<IActionResult> GetQuantidadesDeProcessoPorResposanvel(long processoId, long responsavelId)
        {
            var processoQuery = await _service.GetQueryableAsync();
            var meusProcessos = processoQuery.Count(x => x.ResponsavelId == responsavelId);
            var naoAtribuidos = processoQuery.Count(x => x.ResponsavelId == null);
            var total = meusProcessos + naoAtribuidos;
            return Ok(new {
                meusProcessos,
                naoAtribuidos,
                total
            });
        }

        [HttpGet("{id}/receber/resposavel/{idResponsavel}")]
        public async Task<IActionResult> GetReceber(long id, long idResponsavel)
        {
            var query = await _service.GetQueryableAsync();
            var processo = await query
                .FirstOrDefaultAsync(x => x.Id == id);

            if (processo.ResponsavelId != null)
            {
                throw new ClientErrorException("Processo já tem responsável.", StatusCodes.Status409Conflict);// TODO: Criar validator
            }

            processo.ResponsavelId = idResponsavel;

            await _service.UpdateAsync(processo);

            processo.Responsavel = await _userService.GetByIdAsync(idResponsavel);

            return Ok(processo);
        }
    }
}