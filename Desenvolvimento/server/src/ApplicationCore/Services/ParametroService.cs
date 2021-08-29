using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class ParametroService : BaseService<long, Parametro>, IParametroService
    {
        public ParametroService(IAsyncRepository<long, Parametro> repository, IAppLogger<Parametro> logger)
            : base(repository, logger) { }
    }
}
