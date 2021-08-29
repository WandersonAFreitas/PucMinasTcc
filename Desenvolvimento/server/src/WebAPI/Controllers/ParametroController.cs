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
    public class ParametroController : BaseServiceController<long, Parametro>
    {
        public ParametroController(IParametroService service)
            : base(service) { }
    }
}