using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers.Enums
{
    public enum TagTipoEnum
    {
        [Display(Name = "Texto", Description = "Texto")]
        TEXTO = 0,
        [Display(Name = "Lista", Description = "Lista")]
        LISTA,
        [Display(Name = "Tabela", Description = "Tabela")]
        TABELA,
        [Display(Name = "Pré-definida", Description = "Pré-definida")]
        PRE_DEFINIDA
    }
}
