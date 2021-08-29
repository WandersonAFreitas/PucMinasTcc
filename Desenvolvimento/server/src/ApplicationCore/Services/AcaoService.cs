using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class AcaoService : BaseService<long, Acao>, IAcaoService
    {
        public AcaoService(IAsyncRepository<long, Acao> repository, IAppLogger<Acao> logger)
            : base(repository, logger) { }
    }
}
