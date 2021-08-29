using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class MunicipioService : BaseService<long, Municipio>, IMunicipioService
    {
        public MunicipioService(IAsyncRepository<long, Municipio> repository, IAppLogger<Municipio> logger)
            : base(repository, logger) { }
    }
}
