using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.ViewModels;
using System.Collections.Generic;
using WebAPI.Helpers;
using Microsoft.AspNetCore.Http;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ClientErrorException)
        {
            var mensagem = ((ClientErrorException)context.Exception).Mensagem;
            var errorCode = ((ClientErrorException)context.Exception).ErrorCode;
            var errorList = new string[] { mensagem };
            context.Result = new BadRequestObjectResult(GenerateClientError(errorList, errorCode));
        }
        else
        {
            var ex = context.Exception as Exception;
            var errorList = new List<string>
            {
                ex.Message
            };
            if (ex.Message != null && ex.InnerException != null && ex.InnerException.Message != null)
            {
                errorList.Add(ex.InnerException.Message);
            }

            context.Result = InternalServerError(GenerateClientError(errorList.ToArray(), StatusCodes.Status500InternalServerError));
        }

        base.OnException(context);
    }

    private InternalServerErrorObjectResult InternalServerError()
    {
        return new InternalServerErrorObjectResult();
    }

    private InternalServerErrorObjectResult InternalServerError(object value)
    {
        return new InternalServerErrorObjectResult(value);
    }

    private ClientError GenerateClientError(string[] errorList, int? errorCode = null)
    {
        var clientError = new ClientError(errorList, errorList, errorCode);
        return clientError;
    }
}