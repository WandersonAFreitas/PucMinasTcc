using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TipoMinerioEntityTypeConfiguration : IEntityTypeConfiguration<TipoMinerio>
    {
        public void Configure(EntityTypeBuilder<TipoMinerio> builder)
        {
            builder.ToTable("TipoMinerio", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
