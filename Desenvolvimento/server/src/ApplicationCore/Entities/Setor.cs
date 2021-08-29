using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Setor : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool MesmoEnderecoDaEmpresa { get; set; }

        public long EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? SetorPaiId { get; set; }
        public virtual Setor SetorPai { get; set; }

        public virtual ICollection<Setor> SetoresFilhos { get; set; } = new List<Setor>();
        public virtual ICollection<SetorEndereco> SetorEnderecos { get; set; } = new List<SetorEndereco>();
    }
}
