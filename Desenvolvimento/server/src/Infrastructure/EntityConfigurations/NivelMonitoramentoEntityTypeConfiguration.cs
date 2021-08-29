using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class NivelMonitoramentoEntityTypeConfiguration : IEntityTypeConfiguration<NivelMonitoramento>
    {
        public void Configure(EntityTypeBuilder<NivelMonitoramento> builder)
        {
            builder.ToTable("NivelMonitoramento", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(200);

            builder.Property(a => a.Nivel);
        }
    }
}
