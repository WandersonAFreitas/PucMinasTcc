using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ParametroEntityTypeConfiguration : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.ToTable("Parametro", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id);
            
            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Valor)
                .IsRequired();
        }
    }
}
