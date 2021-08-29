using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class EnderecoService : BaseService<long, Endereco>, IEnderecoService
    {
        public EnderecoService(IAsyncRepository<long, Endereco> repository, IAppLogger<Endereco> logger)
            : base(repository, logger) { }
    }
}
