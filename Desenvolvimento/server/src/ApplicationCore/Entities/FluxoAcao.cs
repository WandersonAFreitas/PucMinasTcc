using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FluxoAcao : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long FluxoId { get; set; }
        public string Nome { get; set; }
        public virtual Fluxo Fluxo { get; set; }
    }
}
