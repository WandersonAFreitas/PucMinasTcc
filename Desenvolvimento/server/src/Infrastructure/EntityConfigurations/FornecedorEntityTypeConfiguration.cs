using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FornecedorEntityTypeConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor", "SCA");

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.RasaoSocial)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.CpfCnpj)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(x => x.CpfCnpj)
                .IsUnique();

            builder.Property(a => a.Telefone)
                .HasMaxLength(20);

            builder.Property(a => a.Celular)
                .HasMaxLength(20);

            builder.Property(a => a.Email)
                .HasMaxLength(200);

            builder.Property(a => a.Site)
                .HasMaxLength(200);

            builder.Property(a => a.Ativo)
                .IsRequired();

            builder.Property(a => a.DataCadastro)
                .IsRequired();

            builder.Property(a => a.UltimaAlteracao);

            builder.Property(a => a.Site)
                .HasMaxLength(200);
        }
    }
}
