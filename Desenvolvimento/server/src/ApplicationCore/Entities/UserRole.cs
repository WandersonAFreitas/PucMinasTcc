using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    public class UserRole : IdentityUserRole<long>
    {
        public virtual Role Role { get; set; }
    }
}
