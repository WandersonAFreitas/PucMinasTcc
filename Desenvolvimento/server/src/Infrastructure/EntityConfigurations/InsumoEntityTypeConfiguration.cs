using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class InsumoEntityTypeConfiguration : IEntityTypeConfiguration<Insumo>
    {
        public void Configure(EntityTypeBuilder<Insumo> builder)
        {
            builder.ToTable("Insumo", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Identificador)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Observacao)
                .HasMaxLength(4000);

            builder.Property(a => a.Modelo)
                .HasMaxLength(200);

            builder.Property(a => a.Patrimonio)
                .HasMaxLength(200);

            builder.Property(a => a.Ativo)
                .IsRequired();

            builder.Property(a => a.DataCriacao)
                .IsRequired();

            builder.Property(a => a.DataInativacao);

            builder.Property(a => a.CriadoPorId);

            builder.Property(a => a.AlteradoPorId);

            builder.Property(a => a.UnidadeMedidaId);

            builder.Property(a => a.MarcaId);

            builder.Property(a => a.TipoInsumoId);

            builder.Property(a => a.SetorId);

            builder.Property(a => a.FornecedorId);
        }
    }
}
