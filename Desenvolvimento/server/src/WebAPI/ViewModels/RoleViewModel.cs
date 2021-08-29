namespace WebAPI.ViewModels
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public long UserId { get; set; }
    }
}