using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Audit
{
    public interface IAudit {
        DateTime AuditDate { get; set; }
        string UserName { get; set; }
        string AuditAction { get; set; }
    }
}
