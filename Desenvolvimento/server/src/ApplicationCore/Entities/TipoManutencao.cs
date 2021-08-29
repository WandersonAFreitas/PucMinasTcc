using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TipoManutencao : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
    }
}
