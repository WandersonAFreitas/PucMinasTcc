using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class UnidadeMedidaEntityTypeConfiguration : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.ToTable("UnidadeMedida", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Sigla)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .HasMaxLength(4000)
                .IsRequired();
        }
    }
}
