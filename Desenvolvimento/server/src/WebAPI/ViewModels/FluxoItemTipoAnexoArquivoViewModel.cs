using ApplicationCore.Entities;
namespace WebAPI.ViewModels
{
    public class FluxoItemTipoAnexoArquivoViewModel
    {
        public long? TramiteArquivoId { get; set; }
        public long? FluxoItemTipoAnexoId { get; set; }
        //public long? ArquivoId { get; set; }
        public Arquivo Arquivo { get; set; }
    }
}