using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class ProcessoService : BaseService<long, Processo>, IProcessoService
    {
        public ProcessoService(IAsyncRepository<long, Processo> repository, IAppLogger<Processo> logger)
            : base(repository, logger) { }
    }
}
