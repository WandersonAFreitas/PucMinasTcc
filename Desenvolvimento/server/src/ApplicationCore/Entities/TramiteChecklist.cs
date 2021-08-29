using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TramiteChecklist : IBaseEntity<long>
    {
        public long Id { get; set; }
        public bool Checado { get; set; }
        public long FluxoItemChecklistId { get; set; }
        public long TramiteId { get; set; }
        public Tramite Tramite { get; set; }
        public FluxoItemChecklist FluxoItemChecklist { get; set; }
    }
}
