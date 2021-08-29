using System.Collections.Generic;
using ApplicationCore.Interfaces.Base;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    public class User : IdentityUser<long>, IBaseEntity<long>
    {
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public virtual ICollection<UserSetor> UserSetores { get; set; } = new List<UserSetor>();
    }
}
