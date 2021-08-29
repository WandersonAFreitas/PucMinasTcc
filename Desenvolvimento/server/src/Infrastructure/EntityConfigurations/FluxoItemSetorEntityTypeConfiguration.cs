using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoItemSetorEntityTypeConfiguration : IEntityTypeConfiguration<FluxoItemSetor>
    {
        public void Configure(EntityTypeBuilder<FluxoItemSetor> builder)
        {
            builder.ToTable("FluxoItemSetor", "SCA");

            builder.HasKey(a => a.Id);
        }
    }
}
