using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class SetorEnderecoEntityTypeConfiguration : IEntityTypeConfiguration<SetorEndereco>
    {
        public void Configure(EntityTypeBuilder<SetorEndereco> builder)
        {
            builder.ToTable("SetorEndereco", "SCA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.SetorId);

            builder.Property(b => b.EnderecoId);
        }
    }
}
