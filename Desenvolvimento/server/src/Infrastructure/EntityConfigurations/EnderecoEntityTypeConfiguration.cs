using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class EnderecoEntityTypeConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.CEP)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(a => a.Logradouro)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Complemento)
                .HasMaxLength(200);

            builder.Property(a => a.Bairro)
                .HasMaxLength(200);

            builder.Property(a => a.Numero)
                .HasMaxLength(20);

            builder.HasMany(e => e.EmpresaEnderecos)
                .WithOne(s => s.Endereco)
                .HasForeignKey(s => s.EnderecoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.SetorEnderecos)
                .WithOne(s => s.Endereco)
                .HasForeignKey(s => s.EnderecoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
