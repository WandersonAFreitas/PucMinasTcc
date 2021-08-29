using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class BarragemEntityTypeConfiguration : IEntityTypeConfiguration<Barragem>
    {
        public void Configure(EntityTypeBuilder<Barragem> builder)
        {
            builder.ToTable("Barragem", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Latitude)
                .IsRequired();

            builder.Property(a => a.Longitude)
                .IsRequired();

            builder.Property(a => a.Posicionamento)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.AlturaAtual)
                .IsRequired();

            builder.Property(a => a.VolumeAtual)
                .IsRequired();

            builder.Property(a => a.CategoriaRisco)
                .IsRequired();

            builder.Property(a => a.DanoPotencialAssociado)
                .IsRequired();

            builder.Property(a => a.Classe)
                .HasMaxLength(200)
                .HasDefaultValue("A")
                .IsRequired();

            builder.Property(b => b.EmpresaId);

            builder.HasOne(e => e.Empresa)
               .WithMany()
               .HasForeignKey(e => e.EmpresaId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.MinerioPrincipalId);

            builder.HasOne(e => e.MinerioPrincipal)
               .WithMany()
               .HasForeignKey(e => e.MinerioPrincipalId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
