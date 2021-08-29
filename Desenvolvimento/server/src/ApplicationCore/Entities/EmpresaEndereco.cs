using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class EmpresaEndereco : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long? EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
