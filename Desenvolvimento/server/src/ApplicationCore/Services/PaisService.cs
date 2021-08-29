using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class PaisService : BaseService<long, Pais>, IPaisService
    {
        public PaisService(IAsyncRepository<long, Pais> repository, IAppLogger<Pais> logger)
            : base(repository, logger) { }
    }
}
