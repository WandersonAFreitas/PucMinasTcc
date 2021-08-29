using ApplicationCore.Interfaces.Base;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Empresa: IBaseEntity<long>
    {
        public Empresa() { }

        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Setor> Setores { get; set; } = new List<Setor>();
        public virtual ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();
        public virtual ICollection<EmpresaEndereco> EmpresaEnderecos { get; set; } = new List<EmpresaEndereco>();
    }
}
