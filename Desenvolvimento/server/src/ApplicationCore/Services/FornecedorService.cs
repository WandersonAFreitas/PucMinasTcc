using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class FornecedorService : BaseService<long, Fornecedor>, IFornecedorService
    {
        public FornecedorService(IAsyncRepository<long, Fornecedor> repository, IAppLogger<Fornecedor> logger)
            : base(repository, logger) { }
    }
}
