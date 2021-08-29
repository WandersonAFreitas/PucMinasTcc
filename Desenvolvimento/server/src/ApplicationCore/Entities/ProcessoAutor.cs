using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ProcessoAutor : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long ProcessoId { get; set; }
        public long AutorId { get; set; }
        public Processo Processo { get; set; }
        public Autor Autor { get; set; }
    }
}
