using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoTipoAnexoEntityTypeConfiguration : IEntityTypeConfiguration<FluxoTipoAnexo>
    {
        public void Configure(EntityTypeBuilder<FluxoTipoAnexo> builder)
        {
            builder.ToTable("FluxoTipoAnexo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.FluxoId);
        }
    }
}
