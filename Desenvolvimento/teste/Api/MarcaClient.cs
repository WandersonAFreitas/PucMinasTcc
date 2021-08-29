using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteWebApi.Model;

namespace TesteWebApi.Api
{
    public partial class ApiClient
    {
        public async Task<List<Marca>> GetMarca()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Marca"));
            return await GetAsync<List<Marca>>(requestUrl);
        }

        public async Task<Message<Marca>> SaveMarca(Marca model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Marca"));
            return await PostAsync<Marca>(requestUrl, model);
        }
    }
}
