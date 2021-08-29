using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class NivelMonitoramento : IBaseEntity<long>
    {
        public long Id { get; set; }

        public string Descricao { get; set; }
        
        public string Observacao { get; set; }

        public NivelMonitoramentoEnum Nivel { get; set; }
        public double ControleDeNivel { get; set; }
    }
}
