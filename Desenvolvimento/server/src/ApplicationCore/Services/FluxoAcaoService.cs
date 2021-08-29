using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoAcaoService : BaseService<long, FluxoAcao>, IFluxoAcaoService
    {
        public FluxoAcaoService(IAsyncRepository<long, FluxoAcao> repository, IAppLogger<FluxoAcao> logger)
            : base(repository, logger) { }
    }
}
