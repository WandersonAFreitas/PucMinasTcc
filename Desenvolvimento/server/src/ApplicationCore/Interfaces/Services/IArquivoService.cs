using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IArquivoService : IService<long, Arquivo>
    {
        Task<Arquivo> GetByHashAsync(string hash);
        Task<List<Arquivo>> GetByHashesAsync(string[] hash);
    }
}
