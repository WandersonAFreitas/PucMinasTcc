using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class PaisEntityTypeConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Sigla)
                .HasMaxLength(2)
                .IsRequired();

            builder.HasMany(u => u.Estados)
                .WithOne(u => u.Pais)
                .HasForeignKey(ur => ur.PaisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Logradouros)
                .WithOne(u => u.Pais)
                .HasForeignKey(ur => ur.PaisId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
