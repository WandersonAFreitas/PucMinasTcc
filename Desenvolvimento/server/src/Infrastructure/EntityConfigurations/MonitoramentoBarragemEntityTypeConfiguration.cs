using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class MonitoramentoBarragemEntityTypeConfiguration : IEntityTypeConfiguration<MonitoramentoBarragem>
    {
        public void Configure(EntityTypeBuilder<MonitoramentoBarragem> builder)
        {
            builder.ToTable("MonitoramentoBarragem", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.BarragemId)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(4000);

            builder.Property(a => a.Nivel)
                .IsRequired();

            builder.Property(a => a.UnidadeMedidaId)
                .IsRequired();

            builder.Property(a => a.DataHora)
                .IsRequired();

            builder.Property(a => a.Latitude);
            builder.Property(a => a.Longitude);

            builder.Property(a => a.SensorId);

            builder.Property(a => a.ConsultoriaId);

            builder.Property(a => a.TipoMonitoramentoId)
                .IsRequired();
        }
    }
}
