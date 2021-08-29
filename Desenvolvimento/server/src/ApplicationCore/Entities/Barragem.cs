using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Barragem : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Posicionamento { get; set; }
        public float AlturaAtual { get; set; }
        public float VolumeAtual { get; set; }
        public string MetodoConstrutivo { get; set; }
        public CategoriaBarragemEnum CategoriaRisco { get; set; }
        public CategoriaBarragemEnum DanoPotencialAssociado { get; set; }
        public string Classe { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }

        public long? MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }

        public long EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public long? MinerioPrincipalId { get; set; }
        public virtual TipoMinerio MinerioPrincipal { get; set; }
    }
}
