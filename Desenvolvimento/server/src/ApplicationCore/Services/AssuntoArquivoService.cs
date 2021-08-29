using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class AssuntoArquivoService : BaseService<long, AssuntoArquivo>, IAssuntoArquivoService
    {
        public AssuntoArquivoService(IAsyncRepository<long, AssuntoArquivo> repository, IAppLogger<AssuntoArquivo> logger)
            : base(repository, logger) { }
    }
}
