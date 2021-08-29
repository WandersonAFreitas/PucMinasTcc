using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Extensions;
using ApplicationCore.Helpers.Pagination;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class InsumoController : BaseServiceController<long, Insumo>
    {
        private readonly UserManager<User> _userManager;
        public InsumoController(IInsumoService service,
                                UserManager<User> userManager)
            : base(service) {
                _userManager = userManager;
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.CriadoPor)
                .Include(x => x.AlteradoPor)
                .Include(x => x.UnidadeMedida)
                .Include(x => x.Marca)
                .Include(x => x.TipoInsumo)
                .Include(x => x.Setor)
                .Include(x => x.Fornecedor)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<Insumo>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var entity = await query
                .Include(x => x.CriadoPor)
                .Include(x => x.AlteradoPor)
                .Include(x => x.UnidadeMedida)
                .Include(x => x.Marca)
                .Include(x => x.TipoInsumo)
                .Include(x => x.Setor)
                .Include(x => x.Fornecedor)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(entity);
        }

        public override async Task<IActionResult> Post([FromBody] Insumo tag)
        {
            tag.DataCriacao = DateTime.Now;
            return Ok(await _service.AddAsync(tag));
        }
    }
}