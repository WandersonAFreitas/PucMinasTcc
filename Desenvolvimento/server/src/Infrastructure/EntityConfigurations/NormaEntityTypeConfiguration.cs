using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class NormaEntityTypeConfiguration : IEntityTypeConfiguration<Norma>
    {
        public void Configure(EntityTypeBuilder<Norma> builder)
        {
            builder.ToTable("Norma", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Codigo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Versao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Origem)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.VigenciaInicial)
                .IsRequired();

            builder.Property(a => a.VigenciaFinal);
        }
    }
}
