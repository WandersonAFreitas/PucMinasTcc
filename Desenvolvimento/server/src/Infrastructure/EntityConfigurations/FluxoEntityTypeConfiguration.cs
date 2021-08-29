using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoEntityTypeConfiguration : IEntityTypeConfiguration<Fluxo>
    {
        public void Configure(EntityTypeBuilder<Fluxo> builder)
        {
            builder.ToTable("Fluxo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(e => e.Ativo)
                .IsRequired();

            builder.HasMany(u => u.Situacoes)
                .WithOne(u => u.Fluxo)
                .HasForeignKey(ur => ur.FluxoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Acoes)
                .WithOne(u => u.Fluxo)
                .HasForeignKey(ur => ur.FluxoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.TiposAnexo)
                .WithOne(u => u.Fluxo)
                .HasForeignKey(ur => ur.FluxoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.FluxoItems)
                .WithOne(u => u.Fluxo)
                .HasForeignKey(ur => ur.FluxoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
