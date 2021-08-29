using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class AssuntoService : BaseService<long, Assunto>, IAssuntoService
    {
        public AssuntoService(IAsyncRepository<long, Assunto> repository, IAppLogger<Assunto> logger)
            : base(repository, logger) { }
    }
}
