using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApplicationCore.Enums
{
    public enum ParametroEnum
    {
        [Description("EMAILESQUECIASENHA")]
        EmailEsqueciASenha = 1,

        [Description("SITUACAOINICIAL")]
        SituacaoInicial = 2,

        [Description("SITUACAOFINAL")]
        SituacaoFinal = 3,

        [Description("EMAILTRAMITEPROCESSO")]
        EmailTramiteProcesso = 4,

        [Description("WEBSERVICEDEFESACIVIL")]
        WebServiceDefesaCivil = 5,

        [Description("WEBSERVICENORMA")]
        WebServiceNorma = 6,

        [Description("WEBSERVICEAQUISICAO")]
        WebServiceAquisicao = 7
    }

    public enum TipoParametroEnum
    {
        [Description("Html")]
        Html = 1,

        [Description("String")]
        String = 2,

        [Description("Inteiro")]
        Inteiro = 3
    }
}
