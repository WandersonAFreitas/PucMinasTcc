using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class MarcaEntityTypeConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.ToTable("Marca", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
