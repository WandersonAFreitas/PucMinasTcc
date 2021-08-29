using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class ProcessoAutorService : BaseService<long, ProcessoAutor>, IProcessoAutorService
    {
        public ProcessoAutorService(IAsyncRepository<long, ProcessoAutor> repository, IAppLogger<ProcessoAutor> logger)
            : base(repository, logger) { }
    }
}
