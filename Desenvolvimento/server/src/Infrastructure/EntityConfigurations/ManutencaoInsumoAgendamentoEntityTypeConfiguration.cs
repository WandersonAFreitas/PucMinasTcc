using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    internal class ManutencaoInsumoAgendamentoEntityTypeConfiguration : IEntityTypeConfiguration<ManutencaoInsumoAgendamento>
    {
        public void Configure(EntityTypeBuilder<ManutencaoInsumoAgendamento> builder)
        {
            builder.ToTable("ManutencaoInsumoAgendamento", "SCA");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ManutencaoInsumoId)
              .IsRequired();
        }
    }
}
