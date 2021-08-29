using System;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public abstract class BaseRestService : IRestService
    {
        public abstract RestClient Client { get; }

        protected string _token;
        private string _header;

        public BaseRestService(IConfiguration configuration, IHttpContextAccessor context, string header = "x-access-token", string token = null)
        {
            _header = header;
            _token = token ?? context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        }

        public T Get<T>(string requestUri) where T : new()
        {
            var request = new RestRequest(requestUri, Method.GET) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.AddHeader(_header, _token);
            }

            var response = Client.Execute<T>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método GET do RestClient");

            return response.Data;
        }


        public async Task<T> GetAsync<T>(string requestUri) where T : new()
        {
            var request = new RestRequest(requestUri, Method.GET) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.AddHeader(_header, _token);
            }

            var response = await Client.ExecuteTaskAsync<T>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método GET do RestClient");

            return response.Data;
        }

        public async Task<byte[]> GetBytesAsync(string requestUri)
        {
            var request = new RestRequest(requestUri, Method.GET) { RequestFormat = DataFormat.Json };

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.AddHeader(_header, _token);
            }

            var response = await Client.ExecuteTaskAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método GET do RestClient");

            return response.RawBytes;
        }

        public T Post<T>(string requestUri, object body = null) where T : new()
        {
            var request = new RestRequest(requestUri, Method.POST) { RequestFormat = DataFormat.Json };

            if (body != null)
            {
                request.AddBody(body);
            }

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.AddHeader(_header, _token);
            }

            var response = Client.Execute<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("Acesso a requisição não autorizado");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método POST do RestClient");

            return response.Data;
        }



        public async Task<T> PostAsync<T>(string requestUri, object body = null) where T : new()
        {
            var request = new RestRequest(requestUri, Method.POST) { RequestFormat = DataFormat.Json };

            if (body != null)
            {
                request.AddBody(body);
            }

            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.AddHeader(_header, _token);
            }

            var response = await Client.ExecuteTaskAsync<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("Acesso a requisição não autorizado");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método POST do RestClient");

            return response.Data;
        }
        public async Task<T> PostUploadAsync<T>(string requestUri, byte[] file, string filename) where T : new()
        {
            var mimetype = "application/octet-stream";
            var request = new RestRequest(requestUri);

            if (string.IsNullOrWhiteSpace(_token))
                throw new Exception("Token vazio.");

            request.Method = Method.POST;
            request.AddHeader(_header, _token);
            request.AddFileBytes(filename, file, filename, mimetype);

            var response = await Client.ExecuteTaskAsync<T>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("Acesso a requisição não autorizado");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
               throw new Exception(response.ErrorMessage ?? response.StatusDescription ?? "Erro ao executar o método POST do RestClient");

            return response.Data;
        }
    }
}