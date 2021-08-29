using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FluxoItemChecklist : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public long FluxoItemId { get; set; }
        public virtual FluxoItem FluxoItem { get; set; }

        public virtual ICollection<TramiteChecklist> TramiteChecklists { get; set; }
    }
}
