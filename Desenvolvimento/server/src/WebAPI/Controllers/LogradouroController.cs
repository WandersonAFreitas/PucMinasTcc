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
    public class LogradouroController : BaseServiceController<long, Logradouro>
    {
        public LogradouroController(ILogradouroService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Pais)
                .Include(x => x.Estado)
                .Include(x => x.Municipio)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Logradouro>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var entity = await query
                .Include(x => x.Pais)
                .Include(x => x.Estado)
                .Include(x => x.Municipio)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(entity);
        }

         [HttpPost("GetLogradouro/{cep}")]
        public async Task<IActionResult> GetLogradouro(string cep)
        {
            var query = await _service.GetQueryableAsync();
            var entity = await query
                .Include(x => x.Pais)
                .Include(x => x.Estado)
                .Include(x => x.Municipio)
                .FirstOrDefaultAsync(x => x.CEP == cep);

            return Ok(entity);
        }
    }
}