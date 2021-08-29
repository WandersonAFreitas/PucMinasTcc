using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Base;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces.Services
{
    public interface ISetorService : IService<long, Setor>
    {
        string RetornaNives(long? setorId);
    }
}
