using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using System.IO;

namespace Infrastructure.Data
{
    public class DaoContextSeed
    {
        public static async Task SeedAsync(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            DaoContext daoContext,
            ILoggerFactory loggerFactory,
            int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {

                // TODO: Only run this if using a real database
                // daoContext.Database.Migrate();

                var roleAdmin = await roleManager.RoleExistsAsync(Roles.ROLE_ADMIN);

                if (!roleAdmin)
                {
                    var resultado = roleManager.CreateAsync(
                        new Role() { Name = Roles.ROLE_ADMIN }).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_ADMIN}.");
                    }
                }

                var admin = new User()
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                    EmailConfirmed = true
                };

                var findAdmin = await userManager.FindByNameAsync(admin.UserName);

                if (findAdmin == null)
                {
                    var resultado = await userManager.CreateAsync(admin, "Pass@word1");
                    if (resultado.Succeeded)
                        userManager.AddToRoleAsync(admin, Roles.ROLE_ADMIN).Wait();
                }

                var roleUser = await roleManager.RoleExistsAsync(Roles.ROLE_USER);

                if (!roleUser)
                {
                    var resultado = roleManager.CreateAsync(
                        new Role() { Name = Roles.ROLE_USER }).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_USER}.");
                    }
                }

                var user = new User()
                {
                    UserName = "user",
                    Email = "user@email.com",
                    EmailConfirmed = true
                };

                var findUser = await userManager.FindByNameAsync(user.UserName);

                if (findUser == null)
                {
                    var resultado = await userManager.CreateAsync(user, "Pass@word1");
                    if (resultado.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, Roles.ROLE_USER).Wait();
                        findUser = await userManager.FindByNameAsync(user.UserName);
                    }
                }

                //SeedSCA(daoContext, findUser);
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<DaoContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(userManager, roleManager, daoContext, loggerFactory, retryForAvailability);
                }
            }
        }

        public static void SeedSCA(DaoContext daoContext, User user)
        {
            #region Essas situações sempre devem existir, não apagar essas inserções
            var situacaoEmElaboracao = new Situacao() { Nome = "Em Elaboração", Padrao = true, TipoSituacao = (int)TipoSituacaoEnum.SituacaoAtual };
            if (daoContext.Situacoes.FirstOrDefault(x => x.Nome == situacaoEmElaboracao.Nome) == null)
                situacaoEmElaboracao.Id = daoContext.Situacoes.Add(situacaoEmElaboracao).Entity.Id;

            var situacaoArquivado = new Situacao() { Nome = "Arquivado", Padrao = true, TipoSituacao = (int)TipoSituacaoEnum.ProximaSituacao };
            if (daoContext.Situacoes.FirstOrDefault(x => x.Nome == situacaoArquivado.Nome) == null)
                situacaoArquivado.Id = daoContext.Situacoes.Add(situacaoArquivado).Entity.Id;

            var situacaoFinalizado = new Situacao() { Nome = "Finalizado", Padrao = true, TipoSituacao = (int)TipoSituacaoEnum.SituacaoFinal };
            if (daoContext.Situacoes.FirstOrDefault(x => x.Nome == situacaoFinalizado.Nome) == null)
                situacaoFinalizado.Id = daoContext.Situacoes.Add(situacaoFinalizado).Entity.Id;
            #endregion

            var empresa = new Empresa() { Ativo = true, Nome = "Mineradora BR", Sigla = "CS" };
            if (daoContext.Empresas.FirstOrDefault(x => x.Nome == empresa.Nome) == null)
            {
                empresa.Id = daoContext.Empresas.Add(empresa).Entity.Id;
            }

            if (empresa.Id != 0)
            {
                var setor = new Setor() { Ativo = true, Nome = "Engenharia", Sigla = "S1", Empresa = empresa };
                if (daoContext.Setores.FirstOrDefault(x => x.Nome == setor.Nome) == null)
                    setor.Id = daoContext.Setores.Add(setor).Entity.Id;

                setor = new Setor() { Ativo = true, Nome = "Administrativo", Sigla = "Adm", Empresa = empresa };
                if (daoContext.Setores.FirstOrDefault(x => x.Nome == setor.Nome) == null)
                    setor.Id = daoContext.Setores.Add(setor).Entity.Id;

                var setorFilho = new Setor() { Ativo = true, Nome = "Contábil", Sigla = "Adm_Contabil", Empresa = empresa, SetorPai = setor };
                if (daoContext.Setores.FirstOrDefault(x => x.Nome == setorFilho.Nome) == null)
                    setorFilho.Id = daoContext.Setores.Add(setorFilho).Entity.Id;

                var assunto = new Assunto() { Ativo = true, Nome = "Controle de barragem", Empresa = empresa };
                if (daoContext.Assuntos.FirstOrDefault(x => x.Nome == assunto.Nome) == null)
                    assunto.Id = daoContext.Assuntos.Add(assunto).Entity.Id;

                assunto = new Assunto() { Ativo = true, Nome = "Verificação de processo", Empresa = empresa };
                if (daoContext.Assuntos.FirstOrDefault(x => x.Nome == assunto.Nome) == null)
                    assunto.Id = daoContext.Assuntos.Add(assunto).Entity.Id;

                var userSetor = new UserSetor() { User = user, Setor = setor };
                if (userSetor.User != null && daoContext.UserSetores.FirstOrDefault(x => x.UserId == user.Id && x.SetorId == setor.Id) == null)
                    userSetor.Id = daoContext.UserSetores.Add(userSetor).Entity.Id;
            }

            #region Parametros
            var parametro = new Parametro() { Nome = ParametroEnum.EmailEsqueciASenha.Descricao(), Valor = "e-mail", Tipo = TipoParametroEnum.Html.Descricao() };
            if (daoContext.Parametros.FirstOrDefault(x => x.Nome == parametro.Nome) == null)
                parametro.Id = daoContext.Parametros.Add(parametro).Entity.Id;

            parametro = new Parametro() { Nome = ParametroEnum.SituacaoInicial.Descricao(), Valor = situacaoEmElaboracao.Nome, Tipo = TipoParametroEnum.String.Descricao() };
            if (daoContext.Parametros.FirstOrDefault(x => x.Nome == parametro.Nome) == null)
                parametro.Id = daoContext.Parametros.Add(parametro).Entity.Id;

            parametro = new Parametro() { Nome = ParametroEnum.SituacaoFinal.Descricao(), Valor = situacaoFinalizado.Nome, Tipo = TipoParametroEnum.String.Descricao() };
            if (daoContext.Parametros.FirstOrDefault(x => x.Nome == parametro.Nome) == null)
                parametro.Id = daoContext.Parametros.Add(parametro).Entity.Id;

            string emailTramiteProcesso = File.ReadAllText(String.Concat(Directory.GetCurrentDirectory().Replace("WebAPI", "Infrastructure"), "\\Data\\FileDaoContextSeed\\EmailTramiteProcesso.txt"));
            parametro = new Parametro() { Nome = ParametroEnum.EmailTramiteProcesso.Descricao(), Valor = emailTramiteProcesso, Tipo = TipoParametroEnum.Html.Descricao() };
            if (daoContext.Parametros.FirstOrDefault(x => x.Nome == parametro.Nome) == null)
                parametro.Id = daoContext.Parametros.Add(parametro).Entity.Id;
            #endregion

            var nivelMonitoramento = new NivelMonitoramento() { Descricao = "Alta", Observacao = "Alta", Nivel = NivelMonitoramentoEnum.Alta, ControleDeNivel = 1000 };
            if (daoContext.NivelMonitoramentos.FirstOrDefault(x => x.Descricao == nivelMonitoramento.Descricao) == null)
                nivelMonitoramento.Id = daoContext.NivelMonitoramentos.Add(nivelMonitoramento).Entity.Id;

            nivelMonitoramento = new NivelMonitoramento() { Descricao = "Media", Observacao = "Media", Nivel = NivelMonitoramentoEnum.Media, ControleDeNivel = 1500 };
            if (daoContext.NivelMonitoramentos.FirstOrDefault(x => x.Descricao == nivelMonitoramento.Descricao) == null)
                nivelMonitoramento.Id = daoContext.NivelMonitoramentos.Add(nivelMonitoramento).Entity.Id;

            nivelMonitoramento = new NivelMonitoramento() { Descricao = "Baixa", Observacao = "Baixa", Nivel = NivelMonitoramentoEnum.Baixa, ControleDeNivel = 2000 };
            if (daoContext.NivelMonitoramentos.FirstOrDefault(x => x.Descricao == nivelMonitoramento.Descricao) == null)
                nivelMonitoramento.Id = daoContext.NivelMonitoramentos.Add(nivelMonitoramento).Entity.Id;


            var sensor = new Sensor() { Identificador = "A00001",  Descricao = "A00001", DataUltimaAfericao = DateTime.Now, TipoSensor = "Automatico", Marca = "XX"  };
            if (daoContext.Sensores.FirstOrDefault(x => x.Identificador == sensor.Identificador) == null)
                sensor.Id = daoContext.Sensores.Add(sensor).Entity.Id;
            
            sensor = new Sensor() { Identificador = "B00001",  Descricao = "B00001", DataUltimaAfericao = DateTime.Now, TipoSensor = "Automatico", Marca = "XX"  };
            if (daoContext.Sensores.FirstOrDefault(x => x.Identificador == sensor.Identificador) == null)
                sensor.Id = daoContext.Sensores.Add(sensor).Entity.Id;

            var acao = new Acao() { Nome = "Alterar a senha" };
            if (daoContext.Acoes.FirstOrDefault(x => x.Nome == acao.Nome) == null)
                acao.Id = daoContext.Acoes.Add(acao).Entity.Id;

            acao = new Acao() { Nome = "Alterar permissão" };
            if (daoContext.Acoes.FirstOrDefault(x => x.Nome == acao.Nome) == null)
                acao.Id = daoContext.Acoes.Add(acao).Entity.Id;

            var tipoAnexo = new TipoAnexo() { Nome = "RG" };
            if (daoContext.TiposAnexo.FirstOrDefault(x => x.Nome == tipoAnexo.Nome) == null)
                tipoAnexo.Id = daoContext.TiposAnexo.Add(tipoAnexo).Entity.Id;

            tipoAnexo = new TipoAnexo() { Nome = "CPF" };
            if (daoContext.TiposAnexo.FirstOrDefault(x => x.Nome == tipoAnexo.Nome) == null)
                tipoAnexo.Id = daoContext.TiposAnexo.Add(tipoAnexo).Entity.Id;

            var tipoInsumo = new TipoInsumo() { Descricao = "Máquina", Ativo = true };
            if (daoContext.TipoInsumos.FirstOrDefault(x => x.Descricao == tipoInsumo.Descricao) == null)
                tipoInsumo.Id = daoContext.TipoInsumos.Add(tipoInsumo).Entity.Id;

            tipoInsumo = new TipoInsumo() { Descricao = "Teste", Ativo = true };
            if (daoContext.TipoInsumos.FirstOrDefault(x => x.Descricao == tipoInsumo.Descricao) == null)
                tipoInsumo.Id = daoContext.TipoInsumos.Add(tipoInsumo).Entity.Id;

            var unidadeMedida = new UnidadeMedida() { Descricao = "Tara", Sigla = "T" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Giga", Sigla = "G" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Mega", Sigla = "M" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Kilo", Sigla = "k" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Hecto", Sigla = "h" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Deca", Sigla = "da" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "BASE", Sigla = "BASE" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Centi", Sigla = "c" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Miti", Sigla = "m" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Micro", Sigla = "u" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            unidadeMedida = new UnidadeMedida() { Descricao = "Nano", Sigla = "n" };
            if (daoContext.UnidadeMedidas.FirstOrDefault(x => x.Descricao == unidadeMedida.Descricao) == null)
                unidadeMedida.Id = daoContext.UnidadeMedidas.Add(unidadeMedida).Entity.Id;

            var tipoMinerio = new TipoMinerio() { Nome = "Ferro" };
            if (daoContext.TipoMinerios.FirstOrDefault(x => x.Nome == tipoMinerio.Nome) == null)
                tipoMinerio.Id = daoContext.TipoMinerios.Add(tipoMinerio).Entity.Id;
                
            tipoMinerio = new TipoMinerio() { Nome = "Pelotas" };
            if (daoContext.TipoMinerios.FirstOrDefault(x => x.Nome == tipoMinerio.Nome) == null)
                tipoMinerio.Id = daoContext.TipoMinerios.Add(tipoMinerio).Entity.Id;

            var barragem = new Barragem()
            {
                Nome = "Barragem 01",
                Latitude = "0",
                Longitude = "1",
                Posicionamento = "Oeste",
                AlturaAtual = 100,
                VolumeAtual = 1500,
                MetodoConstrutivo = "Compactação",
                CategoriaRisco = CategoriaBarragemEnum.Alta,
                DanoPotencialAssociado = CategoriaBarragemEnum.Alta,
                Classe = "A",
                CEP = "60.175-045",
                Logradouro = "Rua Pereira de Miranda",
                Complemento = "Rua",
                Bairro = "Centro",
                Numero = "000001",
                MunicipioId = 1,
                EmpresaId = 1,
                MinerioPrincipalId = 1
            };
            if (daoContext.Barragens.FirstOrDefault(x => x.Nome == barragem.Nome) == null)
                barragem.Id = daoContext.Barragens.Add(barragem).Entity.Id;

            barragem = new Barragem()
            {
                Nome = "Barragem 02",
                Latitude = "5",
                Longitude = "2",
                Posicionamento = "Norte",
                AlturaAtual = 100,
                VolumeAtual = 1500,
                MetodoConstrutivo = "Compactação",
                CategoriaRisco = CategoriaBarragemEnum.Alta,
                DanoPotencialAssociado = CategoriaBarragemEnum.Alta,
                Classe = "A",
                CEP = "60.175-045",
                Logradouro = "Rua Pereira de Miranda",
                Complemento = "Rua",
                Bairro = "Centro",
                Numero = "000001",
                MunicipioId = 1,
                EmpresaId = 1,
                MinerioPrincipalId = 1
            };
            if (daoContext.Barragens.FirstOrDefault(x => x.Nome == barragem.Nome) == null)
                barragem.Id = daoContext.Barragens.Add(barragem).Entity.Id;

            var marca = new Marca() { Descricao = "Honda" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "Valtra" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "John Deere" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "New Holland" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "Dayco" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "Mahle" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "Bosch" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;

            marca = new Marca() { Descricao = "Universal" };
            if (daoContext.Marcas.FirstOrDefault(x => x.Descricao == marca.Descricao) == null)
                marca.Id = daoContext.Marcas.Add(marca).Entity.Id;
           
            var consultoria = new Consultoria() { Nome = "Consultoria 1", Ativo = true, CpfCnpj = "00.000.0000/03",
                                            RasaoSocial = "Consultoria 1", Telefone = "85 90000-0000", DataCadastro = DateTime.Now,
                                            Email = "Consultoria@sca.com.br"};
            if (daoContext.Cosultorias.FirstOrDefault(x => x.Nome == consultoria.Nome) == null)
                consultoria.Id = daoContext.Cosultorias.Add(consultoria).Entity.Id;

            consultoria = new Consultoria() { Nome = "Consultoria 2", Ativo = true, CpfCnpj = "00.000.0000/04",
                                            RasaoSocial = "Consultoria 2", Telefone = "85 90000-0000", DataCadastro = DateTime.Now,
                                            Email = "Consultoria@sca.com.br"};
            if (daoContext.Cosultorias.FirstOrDefault(x => x.Nome == consultoria.Nome) == null)
                consultoria.Id = daoContext.Cosultorias.Add(consultoria).Entity.Id;

            var insumo = new Insumo() {
                Identificador = "A00001",
                Nome = "Insumo A",
                Descricao = "Insumo A referente a máquinas",
                Observacao = "Máquinas de pequenos portes",
                Modelo = "Trator",
                Patrimonio = "A0000011223345a",
                DataCriacao = DateTime.Now,
                CriadoPorId = user.Id,
                AlteradoPorId = user.Id,
                Ativo = true
            };
            if (daoContext.Insumos.FirstOrDefault(x => x.Nome == insumo.Nome) == null)
                insumo.Id = daoContext.Insumos.Add(insumo).Entity.Id;

            insumo = new Insumo()
            {
                Identificador = "A00002",
                Nome = "Insumo B",
                Descricao = "Insumo B referente a máquinas",
                Observacao = "Máquinas de pequenos portes",
                Modelo = "Trator",
                Patrimonio = "A00000112239998b",
                DataCriacao = DateTime.Now,
                CriadoPorId = user.Id,
                AlteradoPorId = user.Id,
                Ativo = true
            };
            if (daoContext.Insumos.FirstOrDefault(x => x.Nome == insumo.Nome) == null)
                insumo.Id = daoContext.Insumos.Add(insumo).Entity.Id;

            var tipoMonitoramento = new TipoMonitoramento() { Nome = "Sensores de Água", Observacao = "Monitoramento de sensores de água" };
            if (daoContext.TipoMonitoramentos.FirstOrDefault(x => x.Nome == tipoMonitoramento.Nome) == null)
                tipoMonitoramento.Id = daoContext.TipoMonitoramentos.Add(tipoMonitoramento).Entity.Id;

            tipoMonitoramento = new TipoMonitoramento() { Nome = "Sensores de Terra", Observacao = "Monitoramento de sensores de Terra" };
            if (daoContext.TipoMonitoramentos.FirstOrDefault(x => x.Nome == tipoMonitoramento.Nome) == null)
                tipoMonitoramento.Id = daoContext.TipoMonitoramentos.Add(tipoMonitoramento).Entity.Id;

            tipoMonitoramento = new TipoMonitoramento() { Nome = "Niveis", Observacao = "Monitoramento de niveis da barragem" };
            if (daoContext.TipoMonitoramentos.FirstOrDefault(x => x.Nome == tipoMonitoramento.Nome) == null)
                tipoMonitoramento.Id = daoContext.TipoMonitoramentos.Add(tipoMonitoramento).Entity.Id;

            var fornecedor = new Fornecedor()
            {
                Nome = "Fornecedor 1",
                Ativo = true,
                CpfCnpj = "00.000.0000/01",
                RasaoSocial = "Fornecedor 1",
                Telefone = "85 90000-0000",
                DataCadastro = DateTime.Now,
                Email = "fornecedor@sca.com.br"
            };
            if (daoContext.Fornecedores.FirstOrDefault(x => x.Nome == fornecedor.Nome) == null)
                fornecedor.Id = daoContext.Fornecedores.Add(fornecedor).Entity.Id;

            fornecedor = new Fornecedor()
            {
                Nome = "Fornecedor 2",
                Ativo = true,
                CpfCnpj = "00.000.0000/02",
                RasaoSocial = "Fornecedor 2",
                Telefone = "85 90000-0000",
                DataCadastro = DateTime.Now,
                Email = "fornecedor@sca.com.br"
            };
            if (daoContext.Fornecedores.FirstOrDefault(x => x.Nome == fornecedor.Nome) == null)
                fornecedor.Id = daoContext.Fornecedores.Add(fornecedor).Entity.Id;

            daoContext.SaveChanges();

            var scripts = Directory.GetFiles(String.Concat(Directory.GetCurrentDirectory().Replace("WebAPI", "Infrastructure"), "\\Data\\SqlDaoContextSeed"), "*.sql");

            foreach (var script in scripts)
                daoContext.Database.ExecuteSqlCommand(File.ReadAllText(script));
        }
    }
}
