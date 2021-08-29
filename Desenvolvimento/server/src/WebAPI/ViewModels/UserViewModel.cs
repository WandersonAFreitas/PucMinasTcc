using System.Collections.Generic;
namespace WebAPI.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public string RolesSting { get; set; }
        public IEnumerable<UserSetorViewModel> UserSetores { get; set; }
        public string UserSetorSting { get; set; }
    }
}