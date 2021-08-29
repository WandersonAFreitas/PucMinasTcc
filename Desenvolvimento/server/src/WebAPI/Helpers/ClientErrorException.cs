using Microsoft.AspNetCore.Http;
using System;
using WebAPI.ViewModels;

namespace WebAPI.Helpers
{
    public class ClientErrorException : Exception
    {
        public ClientErrorException(string mensagem, int? errorCode = StatusCodes.Status400BadRequest)
        {
            this.Mensagem = mensagem;
            this.ErrorCode = errorCode;
        }

        public ClientErrorException(ClientError clientError)
        {
            this.ClientError = clientError;
        }

        public string Mensagem { get; set; }

        public int? ErrorCode { get; set; }

        public ClientError ClientError { get; set; }
    }
}
