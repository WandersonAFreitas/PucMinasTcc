using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class AssuntoArquivo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long AssuntoId { get; set; }
        public virtual Assunto Assunto { get; set; }
        public long ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}
