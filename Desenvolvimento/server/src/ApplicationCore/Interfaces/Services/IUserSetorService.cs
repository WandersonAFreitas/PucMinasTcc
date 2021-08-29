using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IUserSetorService : IService<long, UserSetor>
    {
        Task<List<long>> GetSetoresAsync(long userId);
        Task RemoveFromSetoresAsync(long user, IEnumerable<long> setores);
        Task AddFromSetoresAsync(IEnumerable<UserSetor> setores);
    }
}
