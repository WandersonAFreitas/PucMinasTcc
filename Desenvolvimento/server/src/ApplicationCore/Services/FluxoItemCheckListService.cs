using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FluxoItemCheckListService : BaseService<long, FluxoItemChecklist>, IFluxoItemCheckListService
    {
        public FluxoItemCheckListService(IAsyncRepository<long, FluxoItemChecklist> repository, IAppLogger<FluxoItemChecklist> logger)
            : base(repository, logger) { }
    }
}
