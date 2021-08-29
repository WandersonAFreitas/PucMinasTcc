using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TramiteArquivoEntityTypeConfiguration : IEntityTypeConfiguration<TramiteArquivo>
    {
        public void Configure(EntityTypeBuilder<TramiteArquivo> builder)
        {
            builder.ToTable("TramiteArquivo", "SCA");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.TramiteId, a.ArquivoId })
                .IsUnique();

            builder.Property(a => a.Id);

            builder.Property(b => b.ArquivoId);

            builder.Property(b => b.TramiteId);

            builder.HasOne(e => e.Arquivo)
               .WithMany()
               .HasForeignKey(e => e.ArquivoId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
