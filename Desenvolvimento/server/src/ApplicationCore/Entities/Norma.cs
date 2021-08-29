using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Norma : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Versao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Origem { get; set; }
        public DateTime VigenciaInicial { get; set; }
        public DateTime? VigenciaFinal { get; set; }
    }
}
