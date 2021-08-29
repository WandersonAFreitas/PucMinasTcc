using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class UnidadeMedida : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
    }
}
