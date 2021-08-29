using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FluxoItemTipoAnexo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public bool Obrigatorio { get; set; }
        public bool ExigeAssinaturaDigital { get; set; }

        public long FluxoItemId { get; set; }
        public virtual FluxoItem FluxoItem { get; set; }

        public long FluxoTipoAnexoId { get; set; }
        public virtual FluxoTipoAnexo FluxoTipoAnexo { get; set; }
    }
}
