using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class FluxoService : BaseService<long, Fluxo>, IFluxoService
    {
        public FluxoService(IAsyncRepository<long, Fluxo> repository, IAppLogger<Fluxo> logger)
            : base(repository, logger) { }
    }
}
