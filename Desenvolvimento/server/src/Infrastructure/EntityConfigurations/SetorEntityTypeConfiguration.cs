using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class SetorEntityTypeConfiguration : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder.ToTable("Setor", "SCA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.Sigla)
                .HasMaxLength(30)
                .IsRequired()
                .IsUnicode();

            builder.Property(c => c.Nome)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.MesmoEnderecoDaEmpresa)
                .IsRequired();

            builder.Property(c => c.EmpresaId);

            builder.Property(b => b.SetorPaiId);

            builder
                .HasMany(oj => oj.SetoresFilhos)
                .WithOne(j => j.SetorPai)
                .HasForeignKey(j => j.SetorPaiId);

            builder.HasMany(e => e.SetorEnderecos)
               .WithOne(s => s.Setor)
               .HasForeignKey(s => s.SetorId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
