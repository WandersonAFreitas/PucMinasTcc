using Microsoft.AspNetCore.Http;
namespace WebAPI.ViewModels
{
    public class TramiteArquivoViewModel
    {
        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Hash { get; set; }
        public string Tipo { get; set; }
        public long? TramiteId { get; set; }
        public long? ArquivoId { get; set; }
        public long? FluxoItemTipoAnexoId { get; set; }
        public bool ExigeAssinaturaDigital { get; set; }
        public bool Obrigatorio { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}