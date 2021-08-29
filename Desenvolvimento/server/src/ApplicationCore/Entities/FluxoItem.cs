using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class FluxoItem : IBaseEntity<long>
    {
        public long Id { get; set; }

        public long FluxoId { get; set; }
        public virtual Fluxo Fluxo { get; set; }

        public long SituacaoAtualId { get; set; }
        public virtual FluxoSituacao SituacaoAtual { get; set; }

        public long AcaoId { get; set; }
        public virtual FluxoAcao Acao { get; set; }

        public long ProximaSituacaoId { get; set; }
        public virtual FluxoSituacao ProximaSituacao { get; set; }
        
        public virtual ICollection<FluxoItemSetor> FluxoItemSetores { get; set; } = new List<FluxoItemSetor>();
        public virtual ICollection<FluxoItemTipoAnexo> FluxoItemTiposAnexo { get; set; } = new List<FluxoItemTipoAnexo>();
        public virtual ICollection<FluxoItemChecklist> FluxoItemChecklists { get; set; } = new List<FluxoItemChecklist>();
    }
}
