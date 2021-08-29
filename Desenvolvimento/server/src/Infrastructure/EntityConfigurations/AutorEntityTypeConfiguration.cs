using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class AutorEntityTypeConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor", "SCA");

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.CpfCnpj)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(x => x.CpfCnpj)
                .IsUnique();

            builder.HasMany(e => e.ProcessoAutores)
             .WithOne(s => s.Autor)
             .HasForeignKey(s => s.AutorId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
