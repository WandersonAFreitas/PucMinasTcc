using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Turno : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoralTerminal { get; set; }
    }
}
