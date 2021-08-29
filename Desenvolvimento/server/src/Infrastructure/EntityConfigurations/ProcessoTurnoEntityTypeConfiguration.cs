using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ProcessoTurnoEntityTypeConfiguration : IEntityTypeConfiguration<ProcessoTurno>
    {
        public void Configure(EntityTypeBuilder<ProcessoTurno> builder)
        {
            builder.ToTable("ProcessoTurno", "SCA");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.ProcessoId, a.TurnoId })
                .IsUnique();
        }
    }
}
