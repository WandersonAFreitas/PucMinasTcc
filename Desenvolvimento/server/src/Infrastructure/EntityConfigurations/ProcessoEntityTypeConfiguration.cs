using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ProcessoEntityTypeConfiguration : IEntityTypeConfiguration<Processo>
    {
        public void Configure(EntityTypeBuilder<Processo> builder)
        {
            builder.ToTable("Processo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasIndex(a => new { a.Sequencial, a.Ano })
                .IsUnique();

            builder.HasMany(e => e.Tramites)
             .WithOne(s => s.Processo)
             .HasForeignKey(s => s.ProcessoId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ProcessoAutores)
             .WithOne(s => s.Processo)
             .HasForeignKey(s => s.ProcessoId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
