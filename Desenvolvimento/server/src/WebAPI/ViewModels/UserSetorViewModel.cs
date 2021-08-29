namespace WebAPI.ViewModels
{
    public class UserSetorViewModel
    {
        public string Nome { get; set; }
        public bool Enabled { get; set; }
        public long SetorId { get; set; }
        public long UserId { get; set; }
        public long? SetorPaiId { get; set; }
    }
}