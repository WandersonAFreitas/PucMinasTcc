using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class NivelMonitoramentoService : BaseService<long, NivelMonitoramento>, INivelMonitoramentoService
    {
        public NivelMonitoramentoService(IAsyncRepository<long, NivelMonitoramento> repository, IAppLogger<NivelMonitoramento> logger)
            : base(repository, logger) { }
    }
}
