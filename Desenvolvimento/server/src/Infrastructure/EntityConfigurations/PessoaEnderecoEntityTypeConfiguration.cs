using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class PessoaEnderecoEntityTypeConfiguration : IEntityTypeConfiguration<PessoaEndereco>
    {
        public void Configure(EntityTypeBuilder<PessoaEndereco> builder)
        {
            builder.ToTable("PessoaEndereco", "SCA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.PessoaId);

            builder.Property(b => b.EnderecoId);
        }
    }
}
