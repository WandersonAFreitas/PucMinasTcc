using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoTipoAnexoService : BaseService<long, FluxoTipoAnexo>, IFluxoTipoAnexoService
    {
        public FluxoTipoAnexoService(IAsyncRepository<long, FluxoTipoAnexo> repository, IAppLogger<FluxoTipoAnexo> logger)
            : base(repository, logger) { }
    }
}
