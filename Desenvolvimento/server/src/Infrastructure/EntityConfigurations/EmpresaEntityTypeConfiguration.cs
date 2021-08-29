using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class EmpresaEntityTypeConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa", "SCA");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Sigla)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Nome)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.Ativo)
                .IsRequired();

            builder.HasMany(e => e.Setores)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Assuntos)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.EmpresaEnderecos)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
