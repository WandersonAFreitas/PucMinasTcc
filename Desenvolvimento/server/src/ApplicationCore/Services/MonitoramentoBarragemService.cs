using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class MonitoramentoBarragemService : BaseService<long, MonitoramentoBarragem>, IMonitoramentoBarragemService
    {
        public MonitoramentoBarragemService(IAsyncRepository<long, MonitoramentoBarragem> repository, IAppLogger<MonitoramentoBarragem> logger)
            : base(repository, logger) { }
    }
}
