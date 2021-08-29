using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ManutencaoInsumoEntityTypeConfiguration : IEntityTypeConfiguration<ManutencaoInsumo>
    {
        public void Configure(EntityTypeBuilder<ManutencaoInsumo> builder)
        {
            builder.ToTable("ManutencaoInsumo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Situacao)
                .IsRequired();

            builder.Property(a => a.EmpresaId)
                .IsRequired();

            builder.Property(a => a.SetorId)
                .IsRequired();

            builder.Property(a => a.InsumoId)
                .IsRequired();

            builder.Property(a => a.TipoManutencaoId)
                .IsRequired();
        }
    }
}
