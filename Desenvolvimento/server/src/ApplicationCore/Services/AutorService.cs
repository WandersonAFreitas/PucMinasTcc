using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class AutorService : BaseService<long, Autor>, IAutorService
    {
        public AutorService(IAsyncRepository<long, Autor> repository, IAppLogger<Autor> logger)
            : base(repository, logger) { }
    }
}
