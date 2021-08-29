using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class LogIntegracao : IBaseEntity<long>
    {
        public long Id { get; set; }
        public TipoIntegracaoEnum TipoIntegracao { get; set; }
        public SituacaoIntegracaoEnum Situacao { get; set; }
        public string Ocorrencia { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public DateTime DataHoraFinalizacao { get; set; }
    }
}
