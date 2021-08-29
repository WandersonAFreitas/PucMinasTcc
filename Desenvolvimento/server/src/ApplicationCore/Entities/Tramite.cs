using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Tramite : IBaseEntity<long>
    {
        public long Id { get; set; }
        public bool Tramitado { get; set; }
        public long AcaoId { get; set; }
        public long SetorId { get; set; }
        public long ProcessoId { get; set; }
        public string Observacao { get; set; }
        public long ResponsavelId { get; set; }
        public DateTime? DataTramite { get; set; }
        public bool EnviarEmailAutores { get; set; }
        public string EnviarEmailsPara { get; set; }

        public User Responsavel { get; set; }
        public FluxoAcao Acao { get; set; }
        public Setor Setor { get; set; }
        public Processo Processo { get; set; }

        public long SituacaoDoProcessoNoTramiteId { get; set; }
        public FluxoSituacao SituacaoDoProcessoNoTramite { get; set; } 

        public long SituacaoId { get; set; }
        public FluxoSituacao Situacao { get; set; }

        public virtual ICollection<TramiteArquivo> TramiteArquivos { get; set; }
        public virtual ICollection<TramiteChecklist> TramiteChecklists { get; set; }
    }
}
