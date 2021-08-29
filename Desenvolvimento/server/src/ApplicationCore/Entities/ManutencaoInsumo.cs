using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ManutencaoInsumo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public SituacaoEnum Situacao { get; set; }

        public long? EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? SetorId { get; set; }
        public virtual Setor Setor { get; set; }

        public long? InsumoId { get; set; }
        public virtual Insumo Insumo { get; set; }

        public long? TipoManutencaoId { get; set; }
        public virtual TipoManutencao TipoManutencao { get; set; }

        public virtual ICollection<ManutencaoInsumoItem> ManutencaoInsumoItens { get; set; } = new List<ManutencaoInsumoItem>();
    }
}
