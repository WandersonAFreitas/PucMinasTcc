using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Assunto : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string HashArquivoModelo { get; set; }
        public string Orientacoes { get; set; }

        public long EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? FluxoId { get; set; }
        public virtual Fluxo Fluxo { get; set; }

        public virtual ICollection<AssuntoArquivo> AssuntoArquivos { get; set; }
    }
}
