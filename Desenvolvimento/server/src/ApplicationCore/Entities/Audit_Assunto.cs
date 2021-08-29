using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities.Audit
{
    [Table("Audit_Assunto", Schema = "SCA")]
    public class Audit_Assunto : IBaseEntity<long>, IAudit
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string HashArquivoModelo { get; set; }
        public string Orientacoes { get; set; }

        public long EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? SetorProcessoFisicoId { get; set; }
        public virtual Setor SetorProcessoFisico { get; set; }
                     
        public long? SetorProcessoVirtualId { get; set; }
        public virtual Setor SetorProcessoVirtual { get; set; }

        public DateTime AuditDate { get; set; }
        public string UserName { get; set; }
        public string AuditAction { get; set; }
    }
}
