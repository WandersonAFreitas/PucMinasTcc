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
    public class FluxoItemTipoAnexoValidator : AbstractValidator<FluxoItemTipoAnexo>
    {
        private readonly IFluxoItemTipoAnexoService _service;

        public FluxoItemTipoAnexoValidator(IFluxoItemTipoAnexoService service)
        {
            _service = service;

            RuleFor(x => x.FluxoTipoAnexoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Tipo de Anexo"))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Tipo de Anexo"));

            RuleFor(x => x.FluxoItemId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Fluxo Item"));
        }

        private bool BeUnique(FluxoItemTipoAnexo entity, long FluxoTipoAnexoId)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.FluxoTipoAnexoId == entity.FluxoTipoAnexoId && x.Id != entity.Id);
            return !exist;
        }
    }
}
