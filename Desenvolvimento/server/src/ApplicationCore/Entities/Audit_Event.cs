using ApplicationCore.Interfaces.Base;
using SimpleJson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities.Audit
{
    public class Audit_Event : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Data { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
