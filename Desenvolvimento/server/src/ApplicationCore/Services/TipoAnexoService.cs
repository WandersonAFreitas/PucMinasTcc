using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class TipoAnexoService : BaseService<long, TipoAnexo>, ITipoAnexoService
    {
        public TipoAnexoService(IAsyncRepository<long, TipoAnexo> repository, IAppLogger<TipoAnexo> logger)
            : base(repository, logger) { }
    }
}
