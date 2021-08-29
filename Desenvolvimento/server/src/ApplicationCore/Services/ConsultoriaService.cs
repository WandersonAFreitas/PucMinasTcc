using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class ConsultoriaService : BaseService<long, Consultoria>, IConsultoriaService
    {
        public ConsultoriaService(IAsyncRepository<long, Consultoria> repository, IAppLogger<Consultoria> logger)
            : base(repository, logger) { }
    }
}
