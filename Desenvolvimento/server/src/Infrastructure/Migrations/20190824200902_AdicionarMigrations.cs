using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class AdicionarMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SCA");

            migrationBuilder.CreateTable(
                name: "Acao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audit_Event",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Data = table.Column<string>(type: "jsonb", nullable: false),
                    InsertedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sigla = table.Column<string>(maxLength: 100, nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fluxo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    TramitarEm = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluxo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogIntegracao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TipoIntegracao = table.Column<int>(maxLength: 1, nullable: false),
                    Situacao = table.Column<int>(maxLength: 1, nullable: false),
                    Ocorrencia = table.Column<string>(maxLength: 4000, nullable: false),
                    DataHoraInclusao = table.Column<DateTime>(nullable: false),
                    DataHoraFinalizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogIntegracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NivelMonitoramento",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Observacao = table.Column<string>(maxLength: 200, nullable: true),
                    Nivel = table.Column<int>(nullable: false),
                    ControleDeNivel = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelMonitoramento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Norma",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Codigo = table.Column<string>(maxLength: 200, nullable: false),
                    Versao = table.Column<string>(maxLength: 200, nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Origem = table.Column<string>(maxLength: 200, nullable: false),
                    VigenciaInicial = table.Column<DateTime>(nullable: false),
                    VigenciaFinal = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Norma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Sigla = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Valor = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    CpfCnpj = table.Column<string>(maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(maxLength: 20, nullable: true),
                    Celular = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    RasaoSocial = table.Column<string>(maxLength: 200, nullable: true),
                    Site = table.Column<string>(maxLength: 200, nullable: true),
                    Fornecedor_RasaoSocial = table.Column<string>(maxLength: 200, nullable: true),
                    Fornecedor_Site = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Situacao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Padrao = table.Column<bool>(nullable: false),
                    TipoSituacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAnexo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAnexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoInsumo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInsumo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoManutencao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoManutencao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMinerio",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMinerio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMonitoramento",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Observacao = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMonitoramento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Observacao = table.Column<string>(maxLength: 4000, nullable: false),
                    HoraInicio = table.Column<DateTime>(nullable: false),
                    HoralTerminal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedida",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sigla = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sigla = table.Column<string>(maxLength: 30, nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    MesmoEnderecoDaEmpresa = table.Column<bool>(nullable: false),
                    EmpresaId = table.Column<long>(nullable: false),
                    SetorPaiId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Setor_Setor_SetorPaiId",
                        column: x => x.SetorPaiId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assunto",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    HashArquivoModelo = table.Column<string>(nullable: true),
                    Orientacoes = table.Column<string>(nullable: true),
                    EmpresaId = table.Column<long>(nullable: false),
                    FluxoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assunto_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assunto_Fluxo_FluxoId",
                        column: x => x.FluxoId,
                        principalSchema: "SCA",
                        principalTable: "Fluxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FluxoAcao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FluxoId = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoAcao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoAcao_Fluxo_FluxoId",
                        column: x => x.FluxoId,
                        principalSchema: "SCA",
                        principalTable: "Fluxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoSituacao",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Padrao = table.Column<bool>(nullable: false),
                    Turno = table.Column<bool>(nullable: false),
                    TipoSituacao = table.Column<int>(nullable: false),
                    FluxoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoSituacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoSituacao_Fluxo_FluxoId",
                        column: x => x.FluxoId,
                        principalSchema: "SCA",
                        principalTable: "Fluxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoTipoAnexo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FluxoId = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoTipoAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoTipoAnexo_Fluxo_FluxoId",
                        column: x => x.FluxoId,
                        principalSchema: "SCA",
                        principalTable: "Fluxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Sigla = table.Column<string>(maxLength: 2, nullable: false),
                    PaisId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "SCA",
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Identificador = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    DataUltimaAfericao = table.Column<DateTime>(nullable: false),
                    TipoSensor = table.Column<string>(maxLength: 200, nullable: false),
                    Marca = table.Column<string>(maxLength: 200, nullable: false),
                    ResponsavelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_Pessoa_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<long>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SCA",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arquivo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 600, nullable: false),
                    Extensao = table.Column<string>(maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(maxLength: 100, nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    Hash = table.Column<string>(maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivo_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "SCA",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "SCA",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SCA",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "SCA",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insumo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Identificador = table.Column<string>(maxLength: 200, nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Observacao = table.Column<string>(maxLength: 4000, nullable: true),
                    Modelo = table.Column<string>(maxLength: 200, nullable: true),
                    Patrimonio = table.Column<string>(maxLength: 200, nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataInativacao = table.Column<DateTime>(nullable: true),
                    CriadoPorId = table.Column<long>(nullable: true),
                    AlteradoPorId = table.Column<long>(nullable: true),
                    UnidadeMedidaId = table.Column<long>(nullable: true),
                    MarcaId = table.Column<long>(nullable: true),
                    TipoInsumoId = table.Column<long>(nullable: true),
                    SetorId = table.Column<long>(nullable: true),
                    FornecedorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insumo_Users_AlteradoPorId",
                        column: x => x.AlteradoPorId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_Users_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_Pessoa_FornecedorId",
                        column: x => x.FornecedorId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalSchema: "SCA",
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_TipoInsumo_TipoInsumoId",
                        column: x => x.TipoInsumoId,
                        principalSchema: "SCA",
                        principalTable: "TipoInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insumo_UnidadeMedida_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalSchema: "SCA",
                        principalTable: "UnidadeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSetor",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    SetorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSetor_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSetor_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoItem",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FluxoId = table.Column<long>(nullable: false),
                    SituacaoAtualId = table.Column<long>(nullable: false),
                    AcaoId = table.Column<long>(nullable: false),
                    ProximaSituacaoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoItem_FluxoAcao_AcaoId",
                        column: x => x.AcaoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoAcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoItem_Fluxo_FluxoId",
                        column: x => x.FluxoId,
                        principalSchema: "SCA",
                        principalTable: "Fluxo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoItem_FluxoSituacao_ProximaSituacaoId",
                        column: x => x.ProximaSituacaoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoSituacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoItem_FluxoSituacao_SituacaoAtualId",
                        column: x => x.SituacaoAtualId,
                        principalSchema: "SCA",
                        principalTable: "FluxoSituacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sequencial = table.Column<long>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 2000, nullable: false),
                    UltimaAltercao = table.Column<DateTime>(nullable: false),
                    Criacao = table.Column<DateTime>(nullable: false),
                    EmpresaId = table.Column<long>(nullable: false),
                    AssuntoId = table.Column<long>(nullable: false),
                    SituacaoId = table.Column<long>(nullable: false),
                    ResponsavelId = table.Column<long>(nullable: true),
                    SetorId = table.Column<long>(nullable: true),
                    NormaId = table.Column<long>(nullable: true),
                    ConsultoriaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processo_Assunto_AssuntoId",
                        column: x => x.AssuntoId,
                        principalSchema: "SCA",
                        principalTable: "Assunto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processo_Pessoa_ConsultoriaId",
                        column: x => x.ConsultoriaId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processo_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processo_Norma_NormaId",
                        column: x => x.NormaId,
                        principalSchema: "SCA",
                        principalTable: "Norma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processo_Users_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processo_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processo_FluxoSituacao_SituacaoId",
                        column: x => x.SituacaoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoSituacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipio",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    EstadoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipio_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalSchema: "SCA",
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssuntoArquivo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AssuntoId = table.Column<long>(nullable: false),
                    ArquivoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssuntoArquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssuntoArquivo_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalSchema: "SCA",
                        principalTable: "Arquivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssuntoArquivo_Assunto_AssuntoId",
                        column: x => x.AssuntoId,
                        principalSchema: "SCA",
                        principalTable: "Assunto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManutencaoInsumo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<long>(nullable: false),
                    SetorId = table.Column<long>(nullable: false),
                    InsumoId = table.Column<long>(nullable: false),
                    TipoManutencaoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumo_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalSchema: "SCA",
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumo_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumo_TipoManutencao_TipoManutencaoId",
                        column: x => x.TipoManutencaoId,
                        principalSchema: "SCA",
                        principalTable: "TipoManutencao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoItemChecklist",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    FluxoItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoItemChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoItemChecklist_FluxoItem_FluxoItemId",
                        column: x => x.FluxoItemId,
                        principalSchema: "SCA",
                        principalTable: "FluxoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoItemSetor",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SetorId = table.Column<long>(nullable: false),
                    FluxoItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoItemSetor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoItemSetor_FluxoItem_FluxoItemId",
                        column: x => x.FluxoItemId,
                        principalSchema: "SCA",
                        principalTable: "FluxoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoItemSetor_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FluxoItemTipoAnexo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Obrigatorio = table.Column<bool>(nullable: false),
                    ExigeAssinaturaDigital = table.Column<bool>(nullable: false),
                    FluxoItemId = table.Column<long>(nullable: false),
                    FluxoTipoAnexoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoItemTipoAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FluxoItemTipoAnexo_FluxoItem_FluxoItemId",
                        column: x => x.FluxoItemId,
                        principalSchema: "SCA",
                        principalTable: "FluxoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoItemTipoAnexo_FluxoTipoAnexo_FluxoTipoAnexoId",
                        column: x => x.FluxoTipoAnexoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoTipoAnexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessoAutor",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProcessoId = table.Column<long>(nullable: false),
                    AutorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoAutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessoAutor_Pessoa_AutorId",
                        column: x => x.AutorId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoAutor_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalSchema: "SCA",
                        principalTable: "Processo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessoTurno",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProcessoId = table.Column<long>(nullable: false),
                    TurnoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessoTurno_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalSchema: "SCA",
                        principalTable: "Processo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoTurno_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalSchema: "SCA",
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tramite",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Tramitado = table.Column<bool>(nullable: false),
                    AcaoId = table.Column<long>(nullable: false),
                    SetorId = table.Column<long>(nullable: false),
                    ProcessoId = table.Column<long>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 2000, nullable: false),
                    ResponsavelId = table.Column<long>(nullable: false),
                    DataTramite = table.Column<DateTime>(nullable: true),
                    EnviarEmailAutores = table.Column<bool>(nullable: false),
                    EnviarEmailsPara = table.Column<string>(maxLength: 2000, nullable: false),
                    SituacaoDoProcessoNoTramiteId = table.Column<long>(nullable: false),
                    SituacaoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramite_FluxoAcao_AcaoId",
                        column: x => x.AcaoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoAcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalSchema: "SCA",
                        principalTable: "Processo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_Users_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalSchema: "SCA",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_FluxoSituacao_SituacaoDoProcessoNoTramiteId",
                        column: x => x.SituacaoDoProcessoNoTramiteId,
                        principalSchema: "SCA",
                        principalTable: "FluxoSituacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_FluxoSituacao_SituacaoId",
                        column: x => x.SituacaoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoSituacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barragem",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    Longitude = table.Column<string>(nullable: false),
                    Posicionamento = table.Column<string>(maxLength: 200, nullable: false),
                    AlturaAtual = table.Column<float>(nullable: false),
                    VolumeAtual = table.Column<float>(nullable: false),
                    MetodoConstrutivo = table.Column<string>(nullable: true),
                    CategoriaRisco = table.Column<int>(nullable: false),
                    DanoPotencialAssociado = table.Column<int>(nullable: false),
                    Classe = table.Column<string>(maxLength: 200, nullable: false, defaultValue: "A"),
                    CEP = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    MunicipioId = table.Column<long>(nullable: true),
                    EmpresaId = table.Column<long>(nullable: false),
                    MinerioPrincipalId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barragem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barragem_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Barragem_TipoMinerio_MinerioPrincipalId",
                        column: x => x.MinerioPrincipalId,
                        principalSchema: "SCA",
                        principalTable: "TipoMinerio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Barragem_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalSchema: "SCA",
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CEP = table.Column<string>(maxLength: 15, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 200, nullable: false),
                    Complemento = table.Column<string>(maxLength: 200, nullable: true),
                    Bairro = table.Column<string>(maxLength: 200, nullable: true),
                    Numero = table.Column<string>(maxLength: 20, nullable: true),
                    MunicipioId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalSchema: "SCA",
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logradouro",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CEP = table.Column<string>(maxLength: 15, nullable: false),
                    Endereco = table.Column<string>(maxLength: 200, nullable: false),
                    Bairro = table.Column<string>(maxLength: 200, nullable: true),
                    PaisId = table.Column<long>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    MunicipioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logradouro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logradouro_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalSchema: "SCA",
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logradouro_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalSchema: "SCA",
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logradouro_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "SCA",
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManutencaoInsumoAgendamento",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Data = table.Column<DateTime>(nullable: true),
                    Hora = table.Column<DateTime>(nullable: true),
                    Dia = table.Column<long>(nullable: true),
                    Mes = table.Column<long>(nullable: true),
                    TipoManutencao = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    ManutencaoInsumoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoInsumoAgendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumoAgendamento_ManutencaoInsumo_ManutencaoInsu~",
                        column: x => x.ManutencaoInsumoId,
                        principalSchema: "SCA",
                        principalTable: "ManutencaoInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManutencaoInsumoItem",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Item = table.Column<long>(nullable: false),
                    Cotar = table.Column<bool>(nullable: false),
                    Quantidade = table.Column<float>(nullable: false),
                    PrecoUnidade = table.Column<float>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    UnidadeMedidaId = table.Column<long>(nullable: false),
                    InsumoId = table.Column<long>(nullable: false),
                    AutorId = table.Column<long>(nullable: true),
                    ManutencaoInsumoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoInsumoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumoItem_Pessoa_AutorId",
                        column: x => x.AutorId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumoItem_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalSchema: "SCA",
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumoItem_ManutencaoInsumo_ManutencaoInsumoId",
                        column: x => x.ManutencaoInsumoId,
                        principalSchema: "SCA",
                        principalTable: "ManutencaoInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManutencaoInsumoItem_UnidadeMedida_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalSchema: "SCA",
                        principalTable: "UnidadeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TramiteArquivo",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TramiteId = table.Column<long>(nullable: false),
                    ArquivoId = table.Column<long>(nullable: false),
                    FluxoItemTipoAnexoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TramiteArquivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TramiteArquivo_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalSchema: "SCA",
                        principalTable: "Arquivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TramiteArquivo_FluxoItemTipoAnexo_FluxoItemTipoAnexoId",
                        column: x => x.FluxoItemTipoAnexoId,
                        principalSchema: "SCA",
                        principalTable: "FluxoItemTipoAnexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TramiteArquivo_Tramite_TramiteId",
                        column: x => x.TramiteId,
                        principalSchema: "SCA",
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TramiteChecklist",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Checado = table.Column<bool>(nullable: false),
                    FluxoItemChecklistId = table.Column<long>(nullable: false),
                    TramiteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TramiteChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TramiteChecklist_FluxoItemChecklist_FluxoItemChecklistId",
                        column: x => x.FluxoItemChecklistId,
                        principalSchema: "SCA",
                        principalTable: "FluxoItemChecklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TramiteChecklist_Tramite_TramiteId",
                        column: x => x.TramiteId,
                        principalSchema: "SCA",
                        principalTable: "Tramite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoramentoBarragem",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BarragemId = table.Column<long>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: false),
                    Observacao = table.Column<string>(maxLength: 4000, nullable: true),
                    NivelMonitoramentoId = table.Column<long>(nullable: false),
                    Nivel = table.Column<float>(nullable: false),
                    UnidadeMedidaId = table.Column<long>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    SensorId = table.Column<long>(nullable: true),
                    ConsultoriaId = table.Column<long>(nullable: true),
                    TipoMonitoramentoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoramentoBarragem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalSchema: "SCA",
                        principalTable: "Barragem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_Pessoa_ConsultoriaId",
                        column: x => x.ConsultoriaId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_NivelMonitoramento_NivelMonitoramento~",
                        column: x => x.NivelMonitoramentoId,
                        principalSchema: "SCA",
                        principalTable: "NivelMonitoramento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_Sensor_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "SCA",
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_TipoMonitoramento_TipoMonitoramentoId",
                        column: x => x.TipoMonitoramentoId,
                        principalSchema: "SCA",
                        principalTable: "TipoMonitoramento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonitoramentoBarragem_UnidadeMedida_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalSchema: "SCA",
                        principalTable: "UnidadeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaEndereco",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmpresaId = table.Column<long>(nullable: false),
                    EnderecoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresaEndereco_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "SCA",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaEndereco_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "SCA",
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaEndereco",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PessoaId = table.Column<long>(nullable: true),
                    EnderecoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaEndereco_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "SCA",
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoaEndereco_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "SCA",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetorEndereco",
                schema: "SCA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SetorId = table.Column<long>(nullable: false),
                    EnderecoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetorEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetorEndereco_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "SCA",
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetorEndereco_Setor_SetorId",
                        column: x => x.SetorId,
                        principalSchema: "SCA",
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_UserId",
                schema: "SCA",
                table: "Arquivo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_EmpresaId",
                schema: "SCA",
                table: "Assunto",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assunto_FluxoId",
                schema: "SCA",
                table: "Assunto",
                column: "FluxoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoArquivo_ArquivoId",
                schema: "SCA",
                table: "AssuntoArquivo",
                column: "ArquivoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssuntoArquivo_AssuntoId_ArquivoId",
                schema: "SCA",
                table: "AssuntoArquivo",
                columns: new[] { "AssuntoId", "ArquivoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_EmpresaId",
                schema: "SCA",
                table: "Barragem",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_MinerioPrincipalId",
                schema: "SCA",
                table: "Barragem",
                column: "MinerioPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_MunicipioId",
                schema: "SCA",
                table: "Barragem",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaEndereco_EmpresaId",
                schema: "SCA",
                table: "EmpresaEndereco",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaEndereco_EnderecoId",
                schema: "SCA",
                table: "EmpresaEndereco",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_MunicipioId",
                schema: "SCA",
                table: "Endereco",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_PaisId",
                schema: "SCA",
                table: "Estado",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoAcao_FluxoId",
                schema: "SCA",
                table: "FluxoAcao",
                column: "FluxoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItem_AcaoId",
                schema: "SCA",
                table: "FluxoItem",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItem_FluxoId",
                schema: "SCA",
                table: "FluxoItem",
                column: "FluxoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItem_ProximaSituacaoId",
                schema: "SCA",
                table: "FluxoItem",
                column: "ProximaSituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItem_SituacaoAtualId",
                schema: "SCA",
                table: "FluxoItem",
                column: "SituacaoAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItemChecklist_FluxoItemId",
                schema: "SCA",
                table: "FluxoItemChecklist",
                column: "FluxoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItemSetor_FluxoItemId",
                schema: "SCA",
                table: "FluxoItemSetor",
                column: "FluxoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItemSetor_SetorId",
                schema: "SCA",
                table: "FluxoItemSetor",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItemTipoAnexo_FluxoItemId",
                schema: "SCA",
                table: "FluxoItemTipoAnexo",
                column: "FluxoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoItemTipoAnexo_FluxoTipoAnexoId",
                schema: "SCA",
                table: "FluxoItemTipoAnexo",
                column: "FluxoTipoAnexoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoSituacao_FluxoId",
                schema: "SCA",
                table: "FluxoSituacao",
                column: "FluxoId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoTipoAnexo_FluxoId",
                schema: "SCA",
                table: "FluxoTipoAnexo",
                column: "FluxoId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_AlteradoPorId",
                schema: "SCA",
                table: "Insumo",
                column: "AlteradoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_CriadoPorId",
                schema: "SCA",
                table: "Insumo",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_FornecedorId",
                schema: "SCA",
                table: "Insumo",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_MarcaId",
                schema: "SCA",
                table: "Insumo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_SetorId",
                schema: "SCA",
                table: "Insumo",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_TipoInsumoId",
                schema: "SCA",
                table: "Insumo",
                column: "TipoInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_UnidadeMedidaId",
                schema: "SCA",
                table: "Insumo",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Logradouro_EstadoId",
                schema: "SCA",
                table: "Logradouro",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Logradouro_MunicipioId",
                schema: "SCA",
                table: "Logradouro",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Logradouro_PaisId",
                schema: "SCA",
                table: "Logradouro",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumo_EmpresaId",
                schema: "SCA",
                table: "ManutencaoInsumo",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumo_InsumoId",
                schema: "SCA",
                table: "ManutencaoInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumo_SetorId",
                schema: "SCA",
                table: "ManutencaoInsumo",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumo_TipoManutencaoId",
                schema: "SCA",
                table: "ManutencaoInsumo",
                column: "TipoManutencaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumoAgendamento_ManutencaoInsumoId",
                schema: "SCA",
                table: "ManutencaoInsumoAgendamento",
                column: "ManutencaoInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumoItem_AutorId",
                schema: "SCA",
                table: "ManutencaoInsumoItem",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumoItem_InsumoId",
                schema: "SCA",
                table: "ManutencaoInsumoItem",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumoItem_ManutencaoInsumoId",
                schema: "SCA",
                table: "ManutencaoInsumoItem",
                column: "ManutencaoInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoInsumoItem_UnidadeMedidaId",
                schema: "SCA",
                table: "ManutencaoInsumoItem",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_BarragemId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_ConsultoriaId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "ConsultoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_NivelMonitoramentoId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "NivelMonitoramentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_SensorId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_TipoMonitoramentoId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "TipoMonitoramentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoBarragem_UnidadeMedidaId",
                schema: "SCA",
                table: "MonitoramentoBarragem",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipio_EstadoId",
                schema: "SCA",
                table: "Municipio",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CpfCnpj",
                schema: "SCA",
                table: "Pessoa",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CpfCnpj1",
                schema: "SCA",
                table: "Pessoa",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CpfCnpj2",
                schema: "SCA",
                table: "Pessoa",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_EnderecoId",
                schema: "SCA",
                table: "PessoaEndereco",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_PessoaId",
                schema: "SCA",
                table: "PessoaEndereco",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_AssuntoId",
                schema: "SCA",
                table: "Processo",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ConsultoriaId",
                schema: "SCA",
                table: "Processo",
                column: "ConsultoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_EmpresaId",
                schema: "SCA",
                table: "Processo",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_NormaId",
                schema: "SCA",
                table: "Processo",
                column: "NormaId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ResponsavelId",
                schema: "SCA",
                table: "Processo",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_SetorId",
                schema: "SCA",
                table: "Processo",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_SituacaoId",
                schema: "SCA",
                table: "Processo",
                column: "SituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_Sequencial_Ano",
                schema: "SCA",
                table: "Processo",
                columns: new[] { "Sequencial", "Ano" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoAutor_AutorId",
                schema: "SCA",
                table: "ProcessoAutor",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoAutor_ProcessoId_AutorId",
                schema: "SCA",
                table: "ProcessoAutor",
                columns: new[] { "ProcessoId", "AutorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoTurno_TurnoId",
                schema: "SCA",
                table: "ProcessoTurno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoTurno_ProcessoId_TurnoId",
                schema: "SCA",
                table: "ProcessoTurno",
                columns: new[] { "ProcessoId", "TurnoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "SCA",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "SCA",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_ResponsavelId",
                schema: "SCA",
                table: "Sensor",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_EmpresaId",
                schema: "SCA",
                table: "Setor",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_SetorPaiId",
                schema: "SCA",
                table: "Setor",
                column: "SetorPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_SetorEndereco_EnderecoId",
                schema: "SCA",
                table: "SetorEndereco",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_SetorEndereco_SetorId",
                schema: "SCA",
                table: "SetorEndereco",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_AcaoId",
                schema: "SCA",
                table: "Tramite",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_ProcessoId",
                schema: "SCA",
                table: "Tramite",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_ResponsavelId",
                schema: "SCA",
                table: "Tramite",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_SetorId",
                schema: "SCA",
                table: "Tramite",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_SituacaoDoProcessoNoTramiteId",
                schema: "SCA",
                table: "Tramite",
                column: "SituacaoDoProcessoNoTramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_SituacaoId",
                schema: "SCA",
                table: "Tramite",
                column: "SituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TramiteArquivo_ArquivoId",
                schema: "SCA",
                table: "TramiteArquivo",
                column: "ArquivoId");

            migrationBuilder.CreateIndex(
                name: "IX_TramiteArquivo_FluxoItemTipoAnexoId",
                schema: "SCA",
                table: "TramiteArquivo",
                column: "FluxoItemTipoAnexoId");

            migrationBuilder.CreateIndex(
                name: "IX_TramiteArquivo_TramiteId_ArquivoId",
                schema: "SCA",
                table: "TramiteArquivo",
                columns: new[] { "TramiteId", "ArquivoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TramiteChecklist_FluxoItemChecklistId",
                schema: "SCA",
                table: "TramiteChecklist",
                column: "FluxoItemChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_TramiteChecklist_TramiteId",
                schema: "SCA",
                table: "TramiteChecklist",
                column: "TramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "SCA",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "SCA",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "SCA",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "SCA",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "SCA",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSetor_SetorId",
                schema: "SCA",
                table: "UserSetor",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSetor_UserId",
                schema: "SCA",
                table: "UserSetor",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "AssuntoArquivo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Audit_Event",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "EmpresaEndereco",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoItemSetor",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "LogIntegracao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Logradouro",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "ManutencaoInsumoAgendamento",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "ManutencaoInsumoItem",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "MonitoramentoBarragem",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Parametro",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "PessoaEndereco",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "ProcessoAutor",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "ProcessoTurno",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "SetorEndereco",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Situacao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TipoAnexo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TramiteArquivo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TramiteChecklist",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UserSetor",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "ManutencaoInsumo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Barragem",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "NivelMonitoramento",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Sensor",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TipoMonitoramento",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Turno",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Arquivo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoItemTipoAnexo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoItemChecklist",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Tramite",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Insumo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TipoManutencao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TipoMinerio",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Municipio",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoTipoAnexo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoItem",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Processo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Marca",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "TipoInsumo",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "UnidadeMedida",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Estado",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoAcao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Assunto",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Norma",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Setor",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "FluxoSituacao",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Empresa",
                schema: "SCA");

            migrationBuilder.DropTable(
                name: "Fluxo",
                schema: "SCA");
        }
    }
}
