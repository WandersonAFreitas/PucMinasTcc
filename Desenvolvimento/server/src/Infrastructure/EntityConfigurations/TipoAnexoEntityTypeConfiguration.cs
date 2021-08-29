using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TipoAnexoEntityTypeConfiguration : IEntityTypeConfiguration<TipoAnexo>
    {
        public void Configure(EntityTypeBuilder<TipoAnexo> builder)
        {
            builder.ToTable("TipoAnexo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();           
        }
    }
}
