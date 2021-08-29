using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TesteWebApi.Api
{
    internal static class ApiClientFactory
    {
        private static Uri apiUri;

        private static Lazy<ApiClient> restClient = new Lazy<ApiClient>(
          () => new ApiClient(apiUri),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            //apiUri = new Uri("http://portalbennerh.camed.com.br/SCAbackend/api/");
            apiUri = new Uri("http://localhost:5000/api/");
        }

        public static ApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
