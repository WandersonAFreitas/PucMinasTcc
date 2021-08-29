using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Situacao : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool Padrao { get; set; }
        public int TipoSituacao { get; set; }
    }
}
