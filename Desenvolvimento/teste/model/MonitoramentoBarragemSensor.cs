namespace TesteWebApi.Model
{
    public class MonitoramentoBarragemSensor
    {
        public long CodigoBarragem { get; set; }

        public string Descricao { get; set; }

        public long CodigoNivelMonitoramento { get; set; }

        public float Nivel { get; set; }
        public long CodigoUnidadeMedida { get; set; }
    
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public long CodigoSensor { get; set; }

        public long CodigoTipoMonitoramento { get; set; }
    }
}