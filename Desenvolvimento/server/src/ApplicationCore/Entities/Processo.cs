using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Processo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long Sequencial { get; set; }
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public DateTime UltimaAltercao { get; set; }
        public DateTime Criacao { get; set; }

        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public long AssuntoId { get; set; }
        public Assunto Assunto { get; set; }

        public long SituacaoId { get; set; }
        public FluxoSituacao Situacao { get; set; }

        public long? ResponsavelId { get; set; }
        public User Responsavel { get; set; }

        public long? SetorId { get; set; }
        public Setor Setor { get; set; }

        public long? NormaId { get; set; }
        public Norma Norma { get; set; }

        public long? ConsultoriaId { get; set; }
        public Consultoria Consultoria { get; set; }

        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<ProcessoAutor> ProcessoAutores { get; set; }

    }
}
