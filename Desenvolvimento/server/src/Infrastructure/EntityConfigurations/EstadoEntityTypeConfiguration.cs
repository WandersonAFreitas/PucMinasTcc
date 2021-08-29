using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class EstadoEntityTypeConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Sigla)
                .HasMaxLength(2)
                .IsRequired();

            builder.HasMany(u => u.Municipios)
                .WithOne(u => u.Estado)
                .HasForeignKey(ur => ur.EstadoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Logradouros)
                .WithOne(u => u.Estado)
                .HasForeignKey(ur => ur.EstadoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
