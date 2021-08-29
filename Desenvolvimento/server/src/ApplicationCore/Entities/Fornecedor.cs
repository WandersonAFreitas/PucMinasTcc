using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Fornecedor : Pessoa
    {
        public string RasaoSocial { get; set; }
        public string Site { get; set; }
    }
}
