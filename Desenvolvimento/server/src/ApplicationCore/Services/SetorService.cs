using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Services
{
    public class SetorService : BaseService<long, Setor>, ISetorService
    {
        public SetorService(IAsyncRepository<long, Setor> repository, IAppLogger<Setor> logger)
            : base(repository, logger) { }

        public string RetornaNives(long? setorId)
        {
            var _return = string.Empty;

            if (setorId == null)
                return _return;

            var setor = _repository.GetByIdAsync(setorId.Value).Result;

            if (setor.SetorPaiId != null)
                _return = string.Concat(_return, RetornaNives(setor.SetorPaiId));

            _return = string.Concat(_return, " / ", setor.Nome);

            return _return;
        }
    }
}
