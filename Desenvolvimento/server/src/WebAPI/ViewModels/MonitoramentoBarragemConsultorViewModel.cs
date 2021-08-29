using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.ViewModels
{
    public class MonitoramentoBarragemConsultorViewModel
    {
        public long CodigoBarragem { get; set; }

        public string Descricao { get; set; }

        public long CodigoNivelMonitoramento { get; set; }

        public float Nivel { get; set; }
        public long CodigoUnidadeMedida { get; set; }
    
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string CpfCnpjConsultoria { get; set; }

        public long CodigoTipoMonitoramento { get; set; }
    }
}
