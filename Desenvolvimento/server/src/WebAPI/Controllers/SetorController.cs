using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SetorController : BaseServiceController<long, Setor>
    {
        public SetorController(ISetorService service)
            : base(service) { }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var setor = await query
                .Include(v => v.SetorPai)
                .Include(v => v.SetoresFilhos)
                .ThenInclude(x => x.SetorPai)
                .Include(x => x.Empresa)
                .ThenInclude(x => x.Setores)
                .ThenInclude(x => x.SetoresFilhos)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(setor);
        }
    }
}