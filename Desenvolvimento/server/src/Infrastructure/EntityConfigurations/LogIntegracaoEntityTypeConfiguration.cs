using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class LogIntegracaoEntityTypeConfiguration : IEntityTypeConfiguration<LogIntegracao>
    {
        public void Configure(EntityTypeBuilder<LogIntegracao> builder)
        {
            builder.ToTable("LogIntegracao", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.TipoIntegracao)
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(a => a.Situacao)
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(a => a.Ocorrencia)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(a => a.DataHoraInclusao)
                .IsRequired();

            builder.Property(a => a.DataHoraFinalizacao);
        }
    }
}
