using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class FluxoItemChecklistEntityTypeConfiguration : IEntityTypeConfiguration<FluxoItemChecklist>
    {
        public void Configure(EntityTypeBuilder<FluxoItemChecklist> builder)
        {
            builder.ToTable("FluxoItemChecklist", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(a => a.TramiteChecklists)
                .WithOne(a => a.FluxoItemChecklist)
                .HasForeignKey(ur => ur.FluxoItemChecklistId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
