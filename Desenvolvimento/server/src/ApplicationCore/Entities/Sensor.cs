using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Sensor : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Identificador { get; set; }
        public string Descricao { get; set; }
        public DateTime DataUltimaAfericao { get; set; }
        public string TipoSensor { get; set; }
        public string Marca { get; set; }

        public long? ResponsavelId { get; set; }
        public virtual Autor Responsavel { get; set; }
    }
}
