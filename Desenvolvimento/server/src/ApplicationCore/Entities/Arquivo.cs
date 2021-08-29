using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;

namespace ApplicationCore.Entities
{
    public class Arquivo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public string Tipo { get; set; }
        public byte[] Bytes { get; set; }
        public string Hash { get; set; }
        public DateTime DataCriacao { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
