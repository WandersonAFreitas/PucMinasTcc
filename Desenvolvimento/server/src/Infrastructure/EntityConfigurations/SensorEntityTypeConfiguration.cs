using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class SensorEntityTypeConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensor", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Identificador)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.DataUltimaAfericao)
                .IsRequired();

            builder.Property(a => a.TipoSensor)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Marca)
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(a => a.ResponsavelId);
        }
    }
}
