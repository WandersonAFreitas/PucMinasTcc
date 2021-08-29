using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoItemSetorService : BaseService<long, FluxoItemSetor>, IFluxoItemSetorService
    {
        public FluxoItemSetorService(IAsyncRepository<long, FluxoItemSetor> repository, IAppLogger<FluxoItemSetor> logger)
            : base(repository, logger) { }
    }
}
