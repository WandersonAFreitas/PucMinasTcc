using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected AuthenticatedUserViewModel UserClaim()
        {
            var usuario = this.User.Claims.FirstOrDefault(x => x.Type == "user").Value;
            var authenticatedUserViewModel = JsonConvert.DeserializeObject<AuthenticatedUserViewModel>(usuario);
            return authenticatedUserViewModel;
        }

        protected ClientError GenerateModalStateClientError()
        {
            var errorList = ModelState.Values.Select(v => v.Errors.Select(e => e.ErrorMessage)).SelectMany(x => x);
            var clientError = new ClientError(errorList, errorList, 0);
            return clientError;
        }

        protected ClientError GenerateClientError(string[] errorList)
        {
            var clientError = new ClientError(errorList, errorList, 0);
            return clientError;
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}