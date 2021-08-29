using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Estado : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public long PaisId { get; set; }
        public virtual Pais Pais { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
        public virtual ICollection<Logradouro> Logradouros { get; set; } = new List<Logradouro>();
    }
}
