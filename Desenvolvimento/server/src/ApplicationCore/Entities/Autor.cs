using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Autor : Pessoa
    {
        public DateTime DataNascimento { get; set; }
        
        public virtual ICollection<ProcessoAutor> ProcessoAutores { get; set; }
    }
}
