using ApplicationCore.Entities;
using ApplicationCore.Extensions;
using ApplicationCore.Helpers.Pagination;
using ApplicationCore.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.ViewModels;
using WebAPI.ViewModels.Pagination;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MonitoramentoBarragemController : BaseServiceController<long, MonitoramentoBarragem>
    {
        private readonly IBarragemService _barragemService;
        private readonly IConsultoriaService _consultoriaService;
        private readonly INivelMonitoramentoService _nivelMonitoramentoService;
        private readonly ITipoMonitoramentoService _tipoMonitoramentoService;
        private readonly IUnidadeMedidaService _unidadeMedidaService;
        private readonly ISensorService _sensorService;

        public MonitoramentoBarragemController(
            IMonitoramentoBarragemService service,
            IBarragemService barragemService,
            IConsultoriaService consultoriaService,
            INivelMonitoramentoService nivelMonitoramentoService,
            ITipoMonitoramentoService tipoMonitoramentoService,
            IUnidadeMedidaService unidadeMedidaService,
            ISensorService sensorService
            ) : base(service) 
        { 
            _barragemService = barragemService;
            _consultoriaService = consultoriaService;
            _nivelMonitoramentoService = nivelMonitoramentoService;
            _tipoMonitoramentoService = tipoMonitoramentoService;
            _unidadeMedidaService = unidadeMedidaService;
            _sensorService = sensorService;
        }

        public override async Task<IActionResult> GetFind([FromBody] GridSettings postGrid)
        {
            var query = await _service.GetQueryableAsync();
            var rows = query
                .Include(x => x.Barragem)
                .Include(x => x.Consultoria)
                .Include(x => x.NivelMonitoramento)
                .Include(x => x.Sensor)
                .Include(x => x.TipoMonitoramento)
                .Include(x => x.UnidadeMedida)
                .Where(postGrid, out int totalItens);
            var result = new Pagination<MonitoramentoBarragem>(rows, postGrid.page, postGrid.rows, totalItens);

            return Ok(result);
        }

        public override async Task<IActionResult> Get(long id)
        {
            var query = await _service.GetQueryableAsync();
            var entity = await query
                .Include(x => x.Barragem)
                .Include(x => x.Consultoria)
                .Include(x => x.NivelMonitoramento)
                .Include(x => x.Sensor)
                .Include(x => x.TipoMonitoramento)
                .Include(x => x.UnidadeMedida)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(entity);
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("sensor")]
        public async Task<IActionResult> PostSensor([FromBody] MonitoramentoBarragemSensorViewModel tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var queryBarragem = await _barragemService.GetQueryableAsync();
            var _barragem = await queryBarragem
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoBarragem);
            if (_barragem == null)
                throw new ClientErrorException("Não foi possivel identificar a barragem.", StatusCodes.Status409Conflict);
            
            var querySensorService = await _sensorService.GetQueryableAsync();
            var _sensor = await querySensorService 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoSensor);
            if (_sensor == null)
                throw new ClientErrorException("Não foi possivel identificar o sensor.", StatusCodes.Status409Conflict);
        
            var queryNivelMonitoramento = await _nivelMonitoramentoService.GetQueryableAsync();
            var _nivelMonitoramento = await queryNivelMonitoramento 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoNivelMonitoramento);
            if (_nivelMonitoramento == null)
                throw new ClientErrorException("Não foi possivel identificar nível monitoramento.", StatusCodes.Status409Conflict);

            var queryTipoMonitoramento = await _tipoMonitoramentoService.GetQueryableAsync();
            var _tipoMonitoramento = await queryTipoMonitoramento 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoTipoMonitoramento);
            if (_tipoMonitoramento == null)
                throw new ClientErrorException("Não foi possivel identificar tipo de monitoramento.", StatusCodes.Status409Conflict);

            var queryUnidadeMedida = await _unidadeMedidaService.GetQueryableAsync();
            var _unidadeMedida = await queryUnidadeMedida 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoUnidadeMedida);
            if (_unidadeMedida == null)
                throw new ClientErrorException("Não foi possivel identificar unidade de medida.", StatusCodes.Status409Conflict);

            var newMonitoramentoBarragem = new MonitoramentoBarragem()
            {
                DataHora = DateTime.Now,
                Descricao = tag.Descricao,
                Observacao = tag.Descricao,
                Latitude = tag.Latitude,
                Longitude = tag.Longitude,
                Nivel = tag.Nivel,
                BarragemId = _barragem.Id,
                SensorId = _sensor.Id,
                UnidadeMedidaId = tag.CodigoUnidadeMedida,
                TipoMonitoramentoId = tag.CodigoTipoMonitoramento,
                NivelMonitoramentoId = tag.CodigoNivelMonitoramento
            };

            return Ok(await _service.AddAsync(newMonitoramentoBarragem));
        }

        [Authorize(Roles = Roles.ROLE_ADMIN)]
        [HttpPost("Consultoria")]
        public async Task<IActionResult> PostConsultoria([FromBody] MonitoramentoBarragemConsultorViewModel tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(GenerateModalStateClientError());

            var queryBarragem = await _barragemService.GetQueryableAsync();
            var _barragem = await queryBarragem
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoBarragem);
            if (_barragem == null)
                throw new ClientErrorException("Não foi possivel identificar a barragem.", StatusCodes.Status409Conflict);
            
            var queryConsultoria = await _consultoriaService.GetQueryableAsync();
            var _consultoria = await queryConsultoria 
                .FirstOrDefaultAsync(x => x.CpfCnpj == tag.CpfCnpjConsultoria);
            if (_consultoria == null)
                throw new ClientErrorException("Não foi possivel identificar o consultor.", StatusCodes.Status409Conflict);
        
            var queryNivelMonitoramento = await _nivelMonitoramentoService.GetQueryableAsync();
            var _nivelMonitoramento = await queryNivelMonitoramento 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoNivelMonitoramento);
            if (_nivelMonitoramento == null)
                throw new ClientErrorException("Não foi possivel identificar nível monitoramento.", StatusCodes.Status409Conflict);

            var queryTipoMonitoramento = await _tipoMonitoramentoService.GetQueryableAsync();
            var _tipoMonitoramento = await queryTipoMonitoramento 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoTipoMonitoramento);
            if (_tipoMonitoramento == null)
                throw new ClientErrorException("Não foi possivel identificar tipo de monitoramento.", StatusCodes.Status409Conflict);

            var queryUnidadeMedida = await _unidadeMedidaService.GetQueryableAsync();
            var _unidadeMedida = await queryUnidadeMedida 
                .FirstOrDefaultAsync(x => x.Id == tag.CodigoUnidadeMedida);
            if (_unidadeMedida == null)
                throw new ClientErrorException("Não foi possivel identificar unidade de medida.", StatusCodes.Status409Conflict);

            var newMonitoramentoBarragem = new MonitoramentoBarragem()
            {
                DataHora = DateTime.Now,
                Descricao = tag.Descricao,
                Observacao = tag.Descricao,
                Latitude = tag.Latitude,
                Longitude = tag.Longitude,
                Nivel = tag.Nivel,
                BarragemId = _barragem.Id,
                ConsultoriaId = _consultoria.Id,
                UnidadeMedidaId = tag.CodigoUnidadeMedida,
                TipoMonitoramentoId = tag.CodigoTipoMonitoramento,
                NivelMonitoramentoId = tag.CodigoNivelMonitoramento
            };

            return Ok(await _service.AddAsync(newMonitoramentoBarragem));
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
           
            string allText = System.IO.File.ReadAllText(@"ViewModels\Json\dashboard-analytics.json");
            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(allText);

            #region Gráfico
            double _count = 0;
            jsonObject.widgets.widget1.datasets.ano2019[0].data.Clear();
            for (int i = 0; i < 12; i++)
            {
                var query = await _service.GetQueryableAsync();
                var count = query
                    .Where(x => x.DataHora.Month == i)
                    .Count();

                jsonObject.widgets.widget1.datasets.ano2019[0].data.Add(count);
                _count += count;
            }
            #endregion

            #region Consutorias
            var queryMonitoramentoBarragem = await _service.GetQueryableAsync();

            var countConsultores = queryMonitoramentoBarragem
                .Where(x => x.ConsultoriaId != null)
                .Count();

            jsonObject.widgets.widget2.conversion.value = countConsultores;

            jsonObject.widgets.widget2.conversion.ofTarget = 0;

            if (countConsultores > 0)
                jsonObject.widgets.widget2.conversion.ofTarget = Convert.ToInt32(countConsultores / _count * 100);
            #endregion

            #region Consutorias
            var countSensores = queryMonitoramentoBarragem
                .Where(x => x.SensorId != null)
                .Count();

            jsonObject.widgets.widget3.impressions.value = countSensores.ToString();

            jsonObject.widgets.widget3.impressions.ofTarget = 0;

            if (countSensores > 0)
                jsonObject.widgets.widget3.impressions.ofTarget = Convert.ToInt32(countSensores / _count * 100);
            #endregion

            #region Total
            jsonObject.widgets.widget4.visits.value = Convert.ToInt32(_count);
            #endregion

            var countAlto = queryMonitoramentoBarragem
                .Include(x => x.NivelMonitoramento)
                .Where(x => x.NivelMonitoramento.Descricao == "Alta");

            var countMedio = queryMonitoramentoBarragem
                .Include(x => x.NivelMonitoramento)
                .Where(x => x.NivelMonitoramento.Descricao == "Media");

            var countBaixo = queryMonitoramentoBarragem
                .Include(x => x.NivelMonitoramento)
                .Where(x => x.NivelMonitoramento.Descricao == "Baixa");

            var queryNivelMonitoramento = await _nivelMonitoramentoService.GetQueryableAsync();

            jsonObject.widgets.widget7.devices[0].value = countAlto.Count();
            jsonObject.widgets.widget7.devices[0].change = Math.Round(countAlto.Sum(x => x.Nivel), 2);
            jsonObject.widgets.widget7.devices[0].nivel = queryNivelMonitoramento.FirstOrDefault(x => x.Descricao == "Alta").ControleDeNivel;

            jsonObject.widgets.widget7.devices[1].value = countMedio.Count();
            jsonObject.widgets.widget7.devices[1].change = Math.Round(countMedio.Sum(x => x.Nivel), 2);
            jsonObject.widgets.widget7.devices[1].nivel = queryNivelMonitoramento.FirstOrDefault(x => x.Descricao == "Media").ControleDeNivel;

            jsonObject.widgets.widget7.devices[2].value = countBaixo.Count();
            jsonObject.widgets.widget7.devices[2].change = Math.Round(countBaixo.Sum(x => x.Nivel), 2);
            jsonObject.widgets.widget7.devices[2].nivel = queryNivelMonitoramento.FirstOrDefault(x => x.Descricao == "Baixa").ControleDeNivel;

            return Ok(jsonObject);
        }
    }
}