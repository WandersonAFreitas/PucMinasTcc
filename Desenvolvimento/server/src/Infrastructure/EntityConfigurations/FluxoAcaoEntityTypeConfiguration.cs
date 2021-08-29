using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoAcaoEntityTypeConfiguration : IEntityTypeConfiguration<FluxoAcao>
    {
        public void Configure(EntityTypeBuilder<FluxoAcao> builder)
        {
            builder.ToTable("FluxoAcao", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.FluxoId);
        }
    }
}
