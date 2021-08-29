using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Parametro : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }
    }
}
