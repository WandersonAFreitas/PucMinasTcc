using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Base;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IFluxoItemService : IService<long, FluxoItem>
    {
    }
}
