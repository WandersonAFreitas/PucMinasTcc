using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class UserSetor : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long? UserId { get; set; }
        public virtual User User { get; set; }

        public long? SetorId { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
