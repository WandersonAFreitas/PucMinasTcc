using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoSituacaoEntityTypeConfiguration : IEntityTypeConfiguration<FluxoSituacao>
    {
        public void Configure(EntityTypeBuilder<FluxoSituacao> builder)
        {
            builder.ToTable("FluxoSituacao", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.FluxoId);
        }
    }
}
