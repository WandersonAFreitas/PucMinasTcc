using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class MarcaService : BaseService<long, Marca>, IMarcaService
    {
        public MarcaService(IAsyncRepository<long, Marca> repository, IAppLogger<Marca> logger)
            : base(repository, logger) { }
    }
}
