using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class TipoMonitoramentoService : BaseService<long, TipoMonitoramento>, ITipoMonitoramentoService
    {
        public TipoMonitoramentoService(IAsyncRepository<long, TipoMonitoramento> repository, IAppLogger<TipoMonitoramento> logger)
            : base(repository, logger) { }
    }
}
