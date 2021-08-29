using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class TipoInsumoService : BaseService<long, TipoInsumo>, ITipoInsumoService
    {
        public TipoInsumoService(IAsyncRepository<long, TipoInsumo> repository, IAppLogger<TipoInsumo> logger)
            : base(repository, logger) { }
    }
}
