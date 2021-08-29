using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class ProcessoAutorEntityTypeConfiguration : IEntityTypeConfiguration<ProcessoAutor>
    {
        public void Configure(EntityTypeBuilder<ProcessoAutor> builder)
        {
            builder.ToTable("ProcessoAutor", "SCA");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.ProcessoId, a.AutorId })
                .IsUnique();
        }
    }
}
