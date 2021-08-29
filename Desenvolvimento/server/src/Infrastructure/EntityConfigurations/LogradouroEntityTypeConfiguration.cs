using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class LogradouroEntityTypeConfiguration : IEntityTypeConfiguration<Logradouro>
    {
        public void Configure(EntityTypeBuilder<Logradouro> builder)
        {
            builder.ToTable("Logradouro", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.CEP)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(a => a.Endereco)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Bairro)
                .HasMaxLength(200);
        }
    }
}
