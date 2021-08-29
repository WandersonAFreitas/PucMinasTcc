using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class AssuntoEntityTypeConfiguration : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.ToTable("Assunto", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();           
            
            builder.Property(a => a.Ativo)
                .IsRequired();

            builder.HasMany(a => a.AssuntoArquivos)
                .WithOne(a => a.Assunto)
                .HasForeignKey(ur => ur.AssuntoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
