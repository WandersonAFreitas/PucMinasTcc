using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class MunicipioEntityTypeConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipio", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(u => u.Logradouros)
                .WithOne(u => u.Municipio)
                .HasForeignKey(ur => ur.MunicipioId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
