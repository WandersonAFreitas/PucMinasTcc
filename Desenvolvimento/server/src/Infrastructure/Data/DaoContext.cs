using ApplicationCore.Entities;
using ApplicationCore.Entities.Audit;
using ApplicationCore.Entities.Identity;
using Audit.Core;
using Audit.EntityFramework;
using Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data
{
    public class DaoContext : AuditIdentityDbContext
    <
        User,
        Role,
        long,
        IdentityUserClaim<long>,
        UserRole,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
    >
    {
        public const string DEFAULT_SCHEMA = "SCA";

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<AssuntoArquivo> AssuntoArquivos { get; set; }
        public DbSet<Situacao> Situacoes { get; set; }
        public DbSet<Acao> Acoes { get; set; }
        public DbSet<TipoAnexo> TiposAnexo { get; set; }
        public DbSet<Fluxo> Fluxos { get; set; }
        public DbSet<FluxoItem> FluxoItens { get; set; }
        public DbSet<FluxoItemSetor> FluxoItensSetor { get; set; }
        public DbSet<FluxoItemTipoAnexo> FluxoItensTipoAnexo { get; set; }
        public DbSet<FluxoItemChecklist> FluxoItensChecklist { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<UserSetor> UserSetores { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Processo> Processos { get; set; }
        public DbSet<ProcessoAutor> ProcessosAutores { get; set; }
        public DbSet<Tramite> Tramites { get; set; }
        public DbSet<TramiteArquivo> TramiteArquivos { get; set; }
        public DbSet<TramiteChecklist> TramitesChecklists { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EmpresaEndereco> EmpresaEnderecos { get; set; }
        public DbSet<SetorEndereco> SetorEnderecos { get; set; }
        public DbSet<Barragem> Barragens { get; set; }
        public DbSet<Consultoria> Cosultorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<LogIntegracao> LogIntegracoes { get; set; }
        public DbSet<ManutencaoInsumo> ManutencaoInsumos { get; set; }
        public DbSet<ManutencaoInsumoItem> ManutencaoInsumoItens { get; set; }
        public DbSet<ManutencaoInsumoAgendamento> ManutencaoInsumoAgendamento { get; set; }
        public DbSet<NivelMonitoramento> NivelMonitoramentos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<MonitoramentoBarragem> MonitoramentoBarragens { get; set; }
        public DbSet<Norma> Normas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaEndereco> PessoaEnderecos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<TipoInsumo> TipoInsumos { get; set; }
        public DbSet<TipoManutencao> TipoManutencoes { get; set; }
        public DbSet<TipoMinerio> TipoMinerios { get; set; }
        public DbSet<TipoMonitoramento> TipoMonitoramentos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
        public DbSet<ProcessoTurno> ProcessoTurnos { get; set; }
        
        public DaoContext(DbContextOptions<DaoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customizações devem vir depois de base.OnModelCreating(builder)
            base.OnModelCreating(builder);

            // Define o schema padrão do contexto
            builder.HasDefaultSchema(DEFAULT_SCHEMA);

            #region EntityTypeConfiguration
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");
            #endregion

            #region SCA
            builder.ApplyConfiguration(new EmpresaEntityTypeConfiguration());
            builder.ApplyConfiguration(new SetorEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssuntoEntityTypeConfiguration());
            builder.ApplyConfiguration(new AcaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new SituacaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoAnexoEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoItemChecklistEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoItemTipoAnexoEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoItemSetorEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoSituacaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoAcaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new FluxoTipoAnexoEntityTypeConfiguration());
            builder.ApplyConfiguration(new ArquivoEntityTypeConfiguration());
            builder.ApplyConfiguration(new AssuntoArquivoEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserSetorEntityTypeConfiguration());
            builder.ApplyConfiguration(new ParametroEntityTypeConfiguration());
            builder.ApplyConfiguration(new AutorEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProcessoEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProcessoAutorEntityTypeConfiguration());
            builder.ApplyConfiguration(new TramiteEntityTypeConfiguration());
            builder.ApplyConfiguration(new TramiteArquivoEntityTypeConfiguration());
            builder.ApplyConfiguration(new TramiteChecklistEntityTypeConfiguration());
            builder.ApplyConfiguration(new PaisEntityTypeConfiguration());
            builder.ApplyConfiguration(new EstadoEntityTypeConfiguration());
            builder.ApplyConfiguration(new MunicipioEntityTypeConfiguration());
            builder.ApplyConfiguration(new LogradouroEntityTypeConfiguration());
            builder.ApplyConfiguration(new EnderecoEntityTypeConfiguration());
            builder.ApplyConfiguration(new EmpresaEnderecoEntityTypeConfiguration());
            builder.ApplyConfiguration(new SetorEnderecoEntityTypeConfiguration());
            builder.ApplyConfiguration(new BarragemEntityTypeConfiguration());
            builder.ApplyConfiguration(new ConsultoriaEntityTypeConfiguration());
            builder.ApplyConfiguration(new FornecedorEntityTypeConfiguration());
            builder.ApplyConfiguration(new InsumoEntityTypeConfiguration());
            builder.ApplyConfiguration(new LogIntegracaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new ManutencaoInsumoEntityTypeConfiguration());
            builder.ApplyConfiguration(new ManutencaoInsumoItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new MarcaEntityTypeConfiguration());
            builder.ApplyConfiguration(new MonitoramentoBarragemEntityTypeConfiguration());
            builder.ApplyConfiguration(new NormaEntityTypeConfiguration());
            builder.ApplyConfiguration(new PessoaEntityTypeConfiguration());
            builder.ApplyConfiguration(new PessoaEnderecoEntityTypeConfiguration());
            builder.ApplyConfiguration(new SensorEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoInsumoEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoManutencaoEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoMinerioEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoMonitoramentoEntityTypeConfiguration());
            builder.ApplyConfiguration(new TurnoEntityTypeConfiguration());
            builder.ApplyConfiguration(new UnidadeMedidaEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProcessoTurnoEntityTypeConfiguration());
            builder.ApplyConfiguration(new ManutencaoInsumoAgendamentoEntityTypeConfiguration());
            builder.ApplyConfiguration(new NivelMonitoramentoEntityTypeConfiguration());
            

            #endregion

            #region Audit
            builder.Entity<Audit_Event>().ToTable("Audit_Event", "SCA");
            builder.Entity<Audit_Event>().HasKey(x => x.Id);
            builder.Entity<Audit_Event>().Property(x => x.Data).HasColumnType("jsonb").IsRequired();
            builder.Entity<Audit_Event>().Property(x => x.InsertedDate).HasDefaultValueSql("now()");
            builder.Entity<Audit_Event>().Property(x => x.UpdatedDate).HasDefaultValueSql("now()");
            #endregion
        }
    }
}