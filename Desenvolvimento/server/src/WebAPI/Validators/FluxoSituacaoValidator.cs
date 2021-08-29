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
    public class FluxoSituacaoValidator : AbstractValidator<FluxoSituacao>
    {
        private readonly IFluxoSituacaoService _service;

        public FluxoSituacaoValidator(IFluxoSituacaoService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "A situação"));

            RuleFor(x => x.FluxoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Fluxo"));
        }

        private bool BeUnique(FluxoSituacao entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;

            var exist = query.Any(x => x.Nome == entity.Nome &&
                                       x.FluxoId == entity.FluxoId && 
                                       x.Id != entity.Id);
            
            return !exist;
        }
    }
}
