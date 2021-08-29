using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TipoMonitoramentoEntityTypeConfiguration : IEntityTypeConfiguration<TipoMonitoramento>
    {
        public void Configure(EntityTypeBuilder<TipoMonitoramento> builder)
        {
            builder.ToTable("TipoMonitoramento", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(4000)
                .IsRequired();
        }
    }
}
