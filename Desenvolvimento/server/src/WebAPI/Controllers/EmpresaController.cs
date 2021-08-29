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

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class EmpresaController : BaseServiceController<long, Empresa>
    {
        public EmpresaController(IEmpresaService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query.Include(x => x.Setores)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Empresa>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var empresa = await query
                .Include(v => v.Setores)
                .ThenInclude(x => x.SetoresFilhos)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(empresa);
        }

    }
}