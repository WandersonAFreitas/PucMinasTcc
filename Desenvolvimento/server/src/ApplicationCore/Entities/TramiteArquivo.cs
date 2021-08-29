using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TramiteArquivo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long TramiteId { get; set; }
        public virtual Tramite Tramite { get; set; }
        public long ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
        public long? FluxoItemTipoAnexoId { get; set; }
        public FluxoItemTipoAnexo FluxoItemTipoAnexo { get; set; }
    }
}
