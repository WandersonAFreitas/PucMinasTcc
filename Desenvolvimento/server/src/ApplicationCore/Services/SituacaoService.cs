using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class SituacaoService : BaseService<long, Situacao>, ISituacaoService
    {
        public SituacaoService(IAsyncRepository<long, Situacao> repository, IAppLogger<Situacao> logger)
            : base(repository, logger) { }
    }
}
