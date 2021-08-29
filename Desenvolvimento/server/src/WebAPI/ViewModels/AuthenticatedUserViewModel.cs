namespace WebAPI.ViewModels
{
    public class AuthenticatedUserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; } = new string[] { };
    }
}
