using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class EmpresaService : BaseService<long, Empresa>, IEmpresaService
    {
        public EmpresaService(IAsyncRepository<long, Empresa> repository, IAppLogger<Empresa> logger)
            : base(repository, logger) { }
    }
}
