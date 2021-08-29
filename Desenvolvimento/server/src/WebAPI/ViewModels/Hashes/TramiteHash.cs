using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels.Hashes
{
    public class TramiteHash
    {

        public long TramiteId { get; set; }

        public long SituacaoId { get; set; }

        public long ProcessoId { get; set; }

        public long TimeStamp { get; set; }
    }
}
