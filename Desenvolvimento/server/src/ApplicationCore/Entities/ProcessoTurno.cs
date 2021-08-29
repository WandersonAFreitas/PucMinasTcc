using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ProcessoTurno : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long ProcessoId { get; set; }
        public Processo Processo { get; set; }

        public long TurnoId { get; set; }
        public Turno Turno { get; set; }
    }
}
