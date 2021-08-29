using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteWebApi.Model;

namespace TesteWebApi.Api
{
    public partial class ApiClient
    {
        public async Task<Message<MonitoramentoBarragemSensor>> SendSensor(MonitoramentoBarragemSensor model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "MonitoramentoBarragem/Sensor"));
            
            return await PostAsync<MonitoramentoBarragemSensor>(requestUrl, model);
        }

        public async Task<Message<MonitoramentoBarragemConsultor>> SendConsultoria(MonitoramentoBarragemConsultor model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "MonitoramentoBarragem/Consultoria"));
            
            return await PostAsync<MonitoramentoBarragemConsultor>(requestUrl, model);
        }
    }
}
