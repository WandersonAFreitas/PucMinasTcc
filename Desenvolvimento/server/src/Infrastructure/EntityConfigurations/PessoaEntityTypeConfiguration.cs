using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.CpfCnpj)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.Telefone);

            builder.Property(a => a.Celular)
                .HasMaxLength(15);

            builder.Property(a => a.Ativo)
                .IsRequired();

            builder.Property(a => a.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.DataCadastro)
                .IsRequired();

            builder.Property(a => a.UltimaAlteracao);
        }
    }
}
