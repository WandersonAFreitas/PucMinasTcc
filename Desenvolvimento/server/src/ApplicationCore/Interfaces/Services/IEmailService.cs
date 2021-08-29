namespace ApplicationCore.Interfaces.Services
{
    public interface IEmailService
    {
        void Send(string titulo, string messagem, string[] emails, string fileName = null, byte[] bit = null);
    }
}
