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
using WebAPI.ViewModels;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class EnderecoController : BaseServiceController<long, Endereco>
    {
        public EnderecoController(IEnderecoService service)
            : base(service) { }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            query = query.Include(x => x.Municipio)
                         .Include(x => x.EmpresaEnderecos)
                         .Include(x => x.SetorEnderecos);

            var empresaId = postGrid?.filters?.rules?.FirstOrDefault(x => x.field == "empresaEnderecos.empresaId" && x.op == "eq")?.data ?? null;
            if (!string.IsNullOrEmpty(empresaId))
            {
                query = query.Where(x => x.EmpresaEnderecos != null && 
                                         x.EmpresaEnderecos.Any(c => c.EmpresaId.ToString() == empresaId));
            }

            var setorId = postGrid?.filters?.rules?.FirstOrDefault(x => x.field == "setorEnderecos.setorId" && x.op == "eq")?.data ?? null;
            if (!string.IsNullOrEmpty(setorId))
            {
                query = query.Where(x => x.SetorEnderecos != null &&
                                         x.SetorEnderecos.Any(c => c.SetorId.ToString() == setorId));
            }

            var rows = query 
                .Where(postGrid, out int totalItens);

            var result = new Pagination<Endereco>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var entity = await query
                .Include(x => x.Municipio)
                .Include(x => x.EmpresaEnderecos)
                .Include(x => x.SetorEnderecos)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(entity);
        }
    }
}