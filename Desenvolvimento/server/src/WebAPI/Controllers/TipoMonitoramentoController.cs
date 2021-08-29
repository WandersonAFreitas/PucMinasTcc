using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TipoMonitoramentoController : BaseServiceController<long, TipoMonitoramento>
    {
        public TipoMonitoramentoController(ITipoMonitoramentoService service)
            : base(service) { }
    }
}