using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Helpers.Pagination;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AssuntoController : BaseServiceController<long, Assunto>
    {
        public AssuntoController(IAssuntoService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query.Include(x => x.Empresa).Include(x => x.Fluxo).Include(x => x.AssuntoArquivos).ThenInclude(x => x.Arquivo)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Assunto>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var assunto = await query
                .Include(x => x.Empresa)
                .Include(x => x.Fluxo)
                .ThenInclude(x => x.Situacoes)
                .Include(x => x.AssuntoArquivos)
                .ThenInclude(x => x.Arquivo)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(assunto);
        }

        [HttpGet("desvincularfluxo/{id}")]
        public async Task<IActionResult> DesvinculaFluxo(long id)
        {
            var query = await _service.GetQueryableAsync();
            var _assunto = query.Where(x => x.Id == id).FirstOrDefault();

            _assunto.Fluxo = null;
            _assunto.FluxoId = null;

            await _service.UpdateAsync(_assunto);

            return Ok();
        }
    }
}