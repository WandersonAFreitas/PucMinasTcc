using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Pais : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
        public virtual ICollection<Logradouro> Logradouros { get; set; } = new List<Logradouro>();
    }
}
