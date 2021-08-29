using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TurnoEntityTypeConfiguration : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("Turno", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(a => a.HoraInicio)
                .IsRequired();

            builder.Property(a => a.HoralTerminal)
               .IsRequired();
        }
    }
}
