using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Fluxo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public int TramitarEm { get; set; }

        public virtual ICollection<Assunto> Assuntos { get; set; } = new List<Assunto>();
        public virtual ICollection<FluxoSituacao> Situacoes { get; set; } = new List<FluxoSituacao>();
        public virtual ICollection<FluxoAcao> Acoes { get; set; } = new List<FluxoAcao>();
        public virtual ICollection<FluxoTipoAnexo> TiposAnexo { get; set; } = new List<FluxoTipoAnexo>();
        public virtual ICollection<FluxoItem> FluxoItems { get; set; } = new List<FluxoItem>();
    }
}
