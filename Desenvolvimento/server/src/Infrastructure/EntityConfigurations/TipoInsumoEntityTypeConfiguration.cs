using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TipoInsumoEntityTypeConfiguration : IEntityTypeConfiguration<TipoInsumo>
    {
        public void Configure(EntityTypeBuilder<TipoInsumo> builder)
        {
            builder.ToTable("TipoInsumo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Ativo)
                .IsRequired();
        }
    }
}
