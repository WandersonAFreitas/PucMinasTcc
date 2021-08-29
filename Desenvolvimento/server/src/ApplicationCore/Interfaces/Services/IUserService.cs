using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;

namespace ApplicationCore.Interfaces.Services
{
    public interface IUserService : IService<long, User>
    {
    }
}
