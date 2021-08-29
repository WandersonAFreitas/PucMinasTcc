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
    public class FluxoItemSetorValidator : AbstractValidator<FluxoItemSetor>
    {
        private readonly IFluxoItemSetorService _service;

        public FluxoItemSetorValidator(IFluxoItemSetorService service)
        {
            _service = service;

            RuleFor(x => x.SetorId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Setor"));
                //.Must(BeUnique).WithMessage($"O setor já existe.");

            RuleFor(x => x.FluxoItemId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Fluxo Item"));
        }

        private bool BeUnique(FluxoItemSetor entity, long SetorId)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.SetorId == entity.SetorId && x.Id != entity.Id);
            return !exist;
        }
    }
}
