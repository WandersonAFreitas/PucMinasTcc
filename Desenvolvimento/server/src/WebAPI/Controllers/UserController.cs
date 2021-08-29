using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApplicationCore.Entities.Identity;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserController : BaseServiceController<long, User>
    {
        public UserController(IUserService service)
            : base(service) { }
    }
}