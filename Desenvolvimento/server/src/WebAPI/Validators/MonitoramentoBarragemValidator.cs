using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class MonitoramentoBarragemValidator : AbstractValidator<MonitoramentoBarragem>
    {
        private readonly IMonitoramentoBarragemService _service;

        public MonitoramentoBarragemValidator(IMonitoramentoBarragemService service)
        {
            _service = service;

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Descrição"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 300));

            RuleFor(x => x.NivelMonitoramentoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nível de monitoramento"));
            
            RuleFor(x => x.BarragemId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Barragem"));

            RuleFor(x => x.Nivel)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nível"));
            
            RuleFor(x => x.UnidadeMedidaId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Unidade de medida"));
            
            RuleFor(x => x.DataHora)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Data e hora"));
            
            RuleFor(x => x.TipoMonitoramentoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Tipo de monitoramento"));
            
            // RuleFor(x => x.Sigla)
            //     .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Sigla"))
            //     .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 1))
            //     .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 2));

            // RuleFor(x => x.PaisId)
            //     .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Pais"));
        }

        // private bool BeUnique(Estado entity, string nome)
        // {
        //     var query = _service.GetQueryableAsync().Result;
        //     var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
        //     return !exist;
        // }
    }
}
