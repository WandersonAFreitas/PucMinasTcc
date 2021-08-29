using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Pessoa : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }     
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimaAlteracao { get; set; }

        public virtual ICollection<PessoaEndereco> PessoaEnderecos { get; set; } = new List<PessoaEndereco>();
    }
}
