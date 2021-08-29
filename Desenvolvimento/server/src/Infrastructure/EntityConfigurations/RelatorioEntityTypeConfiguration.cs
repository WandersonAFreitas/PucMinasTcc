using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class RelatorioEntityTypeConfiguration : IEntityTypeConfiguration<Relatorio>
    {
        public void Configure(EntityTypeBuilder<Relatorio> builder)
        {
            builder.ToTable("Relatorio", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
