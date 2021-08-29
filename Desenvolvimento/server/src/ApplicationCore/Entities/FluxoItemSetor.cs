using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FluxoItemSetor : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long SetorId { get; set; }
        public virtual  Setor Setor { get; set; }

        public long FluxoItemId { get; set; }
        public virtual  FluxoItem FluxoItem { get; set; }
    }
}
