using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Insumo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public string Modelo { get; set; }
        public string Patrimonio { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInativacao { get; set; }

        public long? CriadoPorId { get; set; }
        public User CriadoPor { get; set; }

        public long? AlteradoPorId { get; set; }
        public virtual User AlteradoPor { get; set; }

        public long? UnidadeMedidaId { get; set; }
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        public long? MarcaId { get; set; }
        public virtual Marca Marca { get; set; }

        public long? TipoInsumoId { get; set; }
        public virtual TipoInsumo TipoInsumo { get; set; }

        public long? SetorId { get; set; }
        public virtual Setor Setor { get; set; }

        public long? FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
