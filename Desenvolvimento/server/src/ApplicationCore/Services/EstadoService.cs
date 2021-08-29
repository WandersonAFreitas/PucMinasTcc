using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class EstadoService : BaseService<long, Estado>, IEstadoService
    {
        public EstadoService(IAsyncRepository<long, Estado> repository, IAppLogger<Estado> logger)
            : base(repository, logger) { }
    }
}
