using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoSituacaoService : BaseService<long, FluxoSituacao>, IFluxoSituacaoService
    {
        public FluxoSituacaoService(IAsyncRepository<long, FluxoSituacao> repository, IAppLogger<FluxoSituacao> logger)
            : base(repository, logger) { }
    }
}
