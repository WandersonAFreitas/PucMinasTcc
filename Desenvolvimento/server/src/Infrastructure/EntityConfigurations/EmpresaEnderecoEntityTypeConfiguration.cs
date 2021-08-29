using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class EmpresaEnderecoEntityTypeConfiguration : IEntityTypeConfiguration<EmpresaEndereco>
    {
        public void Configure(EntityTypeBuilder<EmpresaEndereco> builder)
        {
            builder.ToTable("EmpresaEndereco", "SCA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.EmpresaId);

            builder.Property(b => b.EnderecoId);
        }
    }
}
