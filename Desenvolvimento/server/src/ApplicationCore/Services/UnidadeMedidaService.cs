using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class UnidadeMedidaService : BaseService<long, UnidadeMedida>, IUnidadeMedidaService
    {
        public UnidadeMedidaService(IAsyncRepository<long, UnidadeMedida> repository, IAppLogger<UnidadeMedida> logger)
            : base(repository, logger) { }
    }
}
