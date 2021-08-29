using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using System.Threading.Tasks;
using System.Linq;
using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Services
{
    public class UserService : BaseService<long, User>, IUserService
    {
        public UserService(IAsyncRepository<long, User> repository, IAppLogger<User> logger)
            : base(repository, logger) { }
    }
}
