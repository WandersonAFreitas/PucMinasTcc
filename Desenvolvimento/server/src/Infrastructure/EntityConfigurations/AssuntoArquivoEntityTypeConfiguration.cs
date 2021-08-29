using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class AssuntoArquivoEntityTypeConfiguration : IEntityTypeConfiguration<AssuntoArquivo>
    {
        public void Configure(EntityTypeBuilder<AssuntoArquivo> builder)
        {
            builder.ToTable("AssuntoArquivo", "SCA");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.AssuntoId, a.ArquivoId })
                .IsUnique();

            builder.Property(a => a.Id);

            builder.Property(b => b.ArquivoId);

            builder.Property(b => b.AssuntoId);

            builder.HasOne(e => e.Arquivo)
               .WithMany()
               .HasForeignKey(e => e.ArquivoId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
