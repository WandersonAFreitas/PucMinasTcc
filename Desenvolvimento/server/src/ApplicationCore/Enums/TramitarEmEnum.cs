using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApplicationCore.Enums
{
    public enum TramitarEmEnum
    {
        [Description("Setores da empresa do processo")]
        SetoresDaEmpresaDoProcesso = 1,

        [Description("Entre setores de todas as empresas")]
        EntreSetoresDeTodasAsEmpresa = 2,

        [Description("Fluxo definido")]
        FluxoDefinido = 3
    }
}
