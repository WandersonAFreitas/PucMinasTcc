using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteWebApi.Model;

namespace TesteWebApi.Api
{
    public partial class ApiClient
    {
        public void Login()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "account/signin"));

            Signin(requestUrl, "admin", "Pass@word1");
        }
    }
}
