using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoItemTipoAnexoEntityTypeConfiguration : IEntityTypeConfiguration<FluxoItemTipoAnexo>
    {
        public void Configure(EntityTypeBuilder<FluxoItemTipoAnexo> builder)
        {
            builder.ToTable("FluxoItemTipoAnexo", "SCA");

            builder.HasKey(a => a.Id);
        }
    }
}
