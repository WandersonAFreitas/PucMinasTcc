using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Endereco : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }

        public long? MunicipioId { get; set; }
        public virtual Municipio Municipio  { get; set; }

        public virtual ICollection<EmpresaEndereco> EmpresaEnderecos { get; set; }
        public virtual ICollection<SetorEndereco> SetorEnderecos { get; set; }
    }
}
