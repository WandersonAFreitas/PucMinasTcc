using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class TramiteService : BaseService<long, Tramite>, ITramiteService
    {
        public TramiteService(IAsyncRepository<long, Tramite> repository, IAppLogger<Tramite> logger)
            : base(repository, logger) { }
    }
}
