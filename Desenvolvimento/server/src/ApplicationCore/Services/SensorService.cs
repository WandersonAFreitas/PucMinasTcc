using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class SensorService : BaseService<long, Sensor>, ISensorService
    {
        public SensorService(IAsyncRepository<long, Sensor> repository, IAppLogger<Sensor> logger)
            : base(repository, logger) { }
    }
}
