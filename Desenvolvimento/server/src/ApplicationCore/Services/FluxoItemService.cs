using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace ApplicationCore.Services
{
    public class FluxoItemService : BaseService<long, FluxoItem>, IFluxoItemService
    {
        public FluxoItemService(IAsyncRepository<long, FluxoItem> repository, IAppLogger<FluxoItem> logger)
            : base(repository, logger) { }
    }
}
