using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfigurations
{
    internal class UserSetorEntityTypeConfiguration : IEntityTypeConfiguration<UserSetor>
    {
        public void Configure(EntityTypeBuilder<UserSetor> builder)
        {
            builder.ToTable("UserSetor", "SCA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.UserId);

            builder.Property(b => b.SetorId);
        }
    }
}
