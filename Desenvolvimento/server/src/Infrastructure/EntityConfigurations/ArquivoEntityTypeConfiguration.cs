using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.EntityConfigurations
{
    internal class ArquivoEntityTypeConfiguration : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("Arquivo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id);

            builder.Property(b => b.Nome)
                .HasMaxLength(600)
                .IsRequired();

            builder.Property(b => b.Extensao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.Tipo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.Bytes)
                .IsRequired();

            builder.Property(a => a.Hash)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.DataCriacao)
                .IsRequired();

            builder.Property(b => b.UserId);
        }
    }
}
