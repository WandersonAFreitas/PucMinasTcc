using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class TramiteArquivoService : BaseService<long, TramiteArquivo>, ITramiteArquivoService
    {
        public TramiteArquivoService(IAsyncRepository<long, TramiteArquivo> repository, IAppLogger<TramiteArquivo> logger)
            : base(repository, logger) { }
    }
}
