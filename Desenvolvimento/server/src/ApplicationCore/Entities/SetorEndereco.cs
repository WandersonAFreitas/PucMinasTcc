using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class SetorEndereco : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long? SetorId { get; set; }
        public virtual Setor Setor { get; set; }

        public long? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
