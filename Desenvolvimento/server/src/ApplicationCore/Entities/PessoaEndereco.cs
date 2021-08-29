using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class PessoaEndereco : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long? PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public long? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
