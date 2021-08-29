using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class InsumoService : BaseService<long, Insumo>, IInsumoService
    {
        public InsumoService(IAsyncRepository<long, Insumo> repository, IAppLogger<Insumo> logger)
            : base(repository, logger) { }
    }
}
