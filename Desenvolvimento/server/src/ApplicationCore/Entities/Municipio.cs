using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Municipio : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public long EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Logradouro> Logradouros { get; set; } = new List<Logradouro>();
    }
}
