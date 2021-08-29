using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TramiteEntityTypeConfiguration : IEntityTypeConfiguration<Tramite>
    {
        public void Configure(EntityTypeBuilder<Tramite> builder)
        {
            builder.ToTable("Tramite", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(e => e.Tramitado)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(a => a.EnviarEmailsPara)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasMany(a => a.TramiteArquivos)
                .WithOne(a => a.Tramite)
                .HasForeignKey(ur => ur.TramiteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.TramiteChecklists)
                .WithOne(a => a.Tramite)
                .HasForeignKey(ur => ur.TramiteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
