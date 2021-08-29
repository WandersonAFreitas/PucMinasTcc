using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class LogradouroService : BaseService<long, Logradouro>, ILogradouroService
    {
        public LogradouroService(IAsyncRepository<long, Logradouro> repository, IAppLogger<Logradouro> logger)
            : base(repository, logger) { }
    }
}
