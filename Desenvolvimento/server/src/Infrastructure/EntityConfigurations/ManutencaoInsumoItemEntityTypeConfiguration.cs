using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ManutencaoInsumoItemEntityTypeConfiguration : IEntityTypeConfiguration<ManutencaoInsumoItem>
    {
        public void Configure(EntityTypeBuilder<ManutencaoInsumoItem> builder)
        {
            builder.ToTable("ManutencaoInsumoItem", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Item)
                .IsRequired();

            builder.Property(a => a.Cotar)
                .IsRequired();

            builder.Property(a => a.Quantidade)
              .IsRequired();

            builder.Property(a => a.PrecoUnidade)
              .IsRequired();

            builder.Property(a => a.Situacao)
              .IsRequired();

            builder.Property(a => a.UnidadeMedidaId)
              .IsRequired();

            builder.Property(a => a.InsumoId)
              .IsRequired();

            builder.Property(a => a.AutorId);

            builder.Property(a => a.ManutencaoInsumoId)
              .IsRequired();
        }
    }
}
