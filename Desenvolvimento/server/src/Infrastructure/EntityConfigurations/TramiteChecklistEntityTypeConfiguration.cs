using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class TramiteChecklistEntityTypeConfiguration : IEntityTypeConfiguration<TramiteChecklist>
    {
        public void Configure(EntityTypeBuilder<TramiteChecklist> builder)
        {
            builder.ToTable("TramiteChecklist", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(e => e.Checado)
                .IsRequired();
        }
    }
}
