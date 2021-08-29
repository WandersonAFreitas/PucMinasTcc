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
    public class AssuntoArquivoController : BaseServiceController<long, AssuntoArquivo>
    {
        public AssuntoArquivoController(IAssuntoArquivoService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Arquivo)
                .Include(x => x.Assunto)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<AssuntoArquivo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var item = await query
                .Include(x => x.Arquivo)
                .Include(x => x.Assunto)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(item);
        }
    }
}