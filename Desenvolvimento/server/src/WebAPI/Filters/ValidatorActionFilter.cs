using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using WebAPI.ViewModels;

public class ValidatorActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!filterContext.ModelState.IsValid)
        {
            filterContext.Result = new BadRequestObjectResult(GenerateModalStateClientError(filterContext.ModelState));
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {

    }

    private ClientError GenerateModalStateClientError(ModelStateDictionary modelState)
    {
        var errorList = modelState.Values.Select(v => v.Errors.Select(e => e.ErrorMessage)).SelectMany(x => x);
        var clientError = new ClientError(errorList, errorList, 400);
        return clientError;
    }
}