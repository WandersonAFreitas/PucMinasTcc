using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using System.Threading.Tasks;
using System.Linq;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
    public class UserSetorService : BaseService<long, UserSetor>, IUserSetorService
    {
        public UserSetorService(IAsyncRepository<long, UserSetor> repository, IAppLogger<UserSetor> logger)
            : base(repository, logger) { }

        public async Task<List<long>> GetSetoresAsync(long userId)
        {
            var query = await _repository.ListQueryableAsync();
            var setores = query.Where(x => x.UserId == userId).Select(x => x.Setor.Id);
            return setores.ToList();
        }

        public async Task RemoveFromSetoresAsync(long user, IEnumerable<long> setores)
        {
            foreach (var id in setores)
            {
                var query = await _repository.ListQueryableAsync();
                var setor = query.Where(x => (x.UserId == user) && (x.SetorId == id)).FirstOrDefault();
                await _repository.DeleteAsync(setor);
            }
        }

        public async Task AddFromSetoresAsync(IEnumerable<UserSetor> setores)
        {
            foreach (var setor in setores)
                await _repository.AddAsync(setor);
        }
    }
}