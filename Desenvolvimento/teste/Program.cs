using System;
using TesteWebApi.Api;
using TesteWebApi.Model;

namespace testewebapi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Teste Webapi!");

            ApiClientFactory.Instance.Login();
            double operador = 1;
            int count = 0; 

            for (int i = 1; i < 1000; i++)
            {
                SendSensor("Sendor 1", "-3.7451746", "-38.4744777", operador * i, 1);  
                SendSensor("Sendor 2", "-3.0000444", "-39.4444444", operador * i * 0.3, 2);
                SendSensor("Sendor 2", "-5.0000444", "-39.4444444", operador * i * 0.2, 3);
                SendSensor("Sendor 2", "-6.0000444", "-39.4444444", operador * i * 0.4, 1);

                SendConsultor("Consultoria 1", "-3.0000444", "-39.4444444", operador * i * 0.4, 1);                  
                SendConsultor("Consultoria 2", "-3.0000444", "-39.4444444", operador * i * 0.3, 2);  

                System.Threading.Thread.Sleep(500);

                count++;
                if (count > 50)
                {
                    if (operador > 0)
                        operador = -1;
                    else
                        operador = 1;

                    count = 0;
                }

                Console.WriteLine(i);
            }
        }

        static void SendSensor(string descricao, string latitude, string longitude, double nivel, long codigoNivelMonitoramento)
        {
            var _monitoramentoBarragem = new MonitoramentoBarragemSensor()
            {
                Descricao = descricao,
                Nivel = Convert.ToSingle(nivel),
                Latitude = latitude,
                Longitude = longitude,
                CodigoBarragem = 1,
                CodigoUnidadeMedida = 1,
                CodigoNivelMonitoramento = codigoNivelMonitoramento,
                CodigoTipoMonitoramento = 2,
                CodigoSensor = 2
            };

            var response = ApiClientFactory.Instance.SendSensor(_monitoramentoBarragem);      
        }

        static void SendConsultor(string descricao, string latitude, string longitude, double nivel, long codigoNivelMonitoramento)
        {
            var _monitoramentoBarragem = new MonitoramentoBarragemConsultor()
            {
                Descricao = descricao,
                Nivel = Convert.ToSingle(nivel),
                Latitude = latitude,
                Longitude = longitude,
                CodigoBarragem = 1,
                CodigoUnidadeMedida = 1,
                CodigoNivelMonitoramento = codigoNivelMonitoramento,
                CodigoTipoMonitoramento = 2,
                CpfCnpjConsultoria = "00.000.0000/03"
            };

            var response = ApiClientFactory.Instance.SendConsultoria(_monitoramentoBarragem);  
        }
    }
}
