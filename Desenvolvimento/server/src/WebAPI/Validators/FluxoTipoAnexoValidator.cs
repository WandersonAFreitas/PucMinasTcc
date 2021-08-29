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
    public class FluxoTipoAnexoValidator : AbstractValidator<FluxoTipoAnexo>
    {
        private readonly IFluxoTipoAnexoService _service;

        public FluxoTipoAnexoValidator(IFluxoTipoAnexoService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(2).WithMessage(string.Format(Resources.MaximumLength, "Nome", 2))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O tipo de anexo"));

            RuleFor(x => x.FluxoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Fluxo"));
        }

        private bool BeUnique(FluxoTipoAnexo entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
