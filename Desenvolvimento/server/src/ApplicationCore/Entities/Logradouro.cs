using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Logradouro : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        
        public long? PaisId { get; set; }
        public virtual Pais Pais { get; set; }

        public long? EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public long? MunicipioId { get; set; }
        public virtual Municipio Municipio  { get; set; }
    }
}
