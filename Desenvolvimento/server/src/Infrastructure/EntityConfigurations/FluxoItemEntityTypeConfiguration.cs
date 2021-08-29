using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoItemEntityTypeConfiguration : IEntityTypeConfiguration<FluxoItem>
    {
        public void Configure(EntityTypeBuilder<FluxoItem> builder)
        {
            builder.ToTable("FluxoItem", "SCA");

            builder.HasKey(a => a.Id);

            builder.HasMany(u => u.FluxoItemSetores)
                .WithOne(u => u.FluxoItem)
                .HasForeignKey(ur => ur.FluxoItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.FluxoItemTiposAnexo)
                .WithOne(u => u.FluxoItem)
                .HasForeignKey(ur => ur.FluxoItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.FluxoItemChecklists)
                .WithOne(u => u.FluxoItem)
                .HasForeignKey(ur => ur.FluxoItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
