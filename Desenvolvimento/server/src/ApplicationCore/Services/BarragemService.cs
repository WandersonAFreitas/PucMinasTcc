using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class BarragemService : BaseService<long, Barragem>, IBarragemService
    {
        public BarragemService(IAsyncRepository<long, Barragem> repository, IAppLogger<Barragem> logger)
            : base(repository, logger) { }
    }
}
