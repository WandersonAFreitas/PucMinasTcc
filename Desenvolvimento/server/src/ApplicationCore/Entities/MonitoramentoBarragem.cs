using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class MonitoramentoBarragem : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long BarragemId { get; set; }
        public virtual Barragem Barragem { get; set; }

        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public long NivelMonitoramentoId { get; set; }
        public virtual NivelMonitoramento NivelMonitoramento { get; set; }

        public float Nivel { get; set; }
        public long? UnidadeMedidaId { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public DateTime DataHora { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public long? SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }

        public long? ConsultoriaId { get; set; }
        public virtual Consultoria Consultoria { get; set; }

        public long TipoMonitoramentoId { get; set; }
        public virtual TipoMonitoramento TipoMonitoramento { get; set; }

    }
}
