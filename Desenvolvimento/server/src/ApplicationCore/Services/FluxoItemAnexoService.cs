using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoItemTipoAnexoService : BaseService<long, FluxoItemTipoAnexo>, IFluxoItemTipoAnexoService
    {
        public FluxoItemTipoAnexoService(IAsyncRepository<long, FluxoItemTipoAnexo> repository, IAppLogger<FluxoItemTipoAnexo> logger)
            : base(repository, logger) { }
    }
}
