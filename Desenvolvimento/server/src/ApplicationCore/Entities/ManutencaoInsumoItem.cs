using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ManutencaoInsumoItem : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long Item { get; set; }
        public bool Cotar { get; set; }
        public float Quantidade { get; set; }
        public float PrecoUnidade { get; set; }
        public SituacaoEnum Situacao { get; set; }

        public long? UnidadeMedidaId { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public long? InsumoId { get; set; }
        public virtual Insumo Insumo { get; set; }

        public long? AutorId { get; set; }
        public virtual Autor Autor { get; set; }

        public long? ManutencaoInsumoId { get; set; }
        public virtual ManutencaoInsumo ManutencaoInsumo { get; set; }
    }
}
