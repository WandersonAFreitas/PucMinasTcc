using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ManutencaoInsumoAgendamento : IBaseEntity<long>
    {
        public long Id { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        public long? Dia { get; set; }
        public long? Mes { get; set; }
        public TipoAgendamentoEnum TipoManutencao { get; set; }
        public SituacaoEnum Situacao { get; set; }

        public long ManutencaoInsumoId { get; set; }
        public virtual ManutencaoInsumo ManutencaoInsumo { get; set; }
    }
}
