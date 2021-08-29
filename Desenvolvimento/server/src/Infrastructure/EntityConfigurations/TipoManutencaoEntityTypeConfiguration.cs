using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TipoManutencaoEntityTypeConfiguration : IEntityTypeConfiguration<TipoManutencao>
    {
        public void Configure(EntityTypeBuilder<TipoManutencao> builder)
        {
            builder.ToTable("TipoManutencao", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
