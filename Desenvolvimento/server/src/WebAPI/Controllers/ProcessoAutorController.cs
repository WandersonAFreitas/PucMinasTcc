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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProcessoAutorController : BaseServiceController<long, ProcessoAutor>
    {
        public ProcessoAutorController(IProcessoAutorService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Processo)
                .Include(x => x.Autor)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<ProcessoAutor>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var processo = await query
                .Include(x => x.Processo)
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(processo);
        }
    }
}