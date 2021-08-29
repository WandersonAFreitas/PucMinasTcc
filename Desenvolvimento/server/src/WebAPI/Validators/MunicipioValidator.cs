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
    public class MunicipioValidator : AbstractValidator<Municipio>
    {
        private readonly IMunicipioService _service;

        public MunicipioValidator(IMunicipioService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Nome do Município"));

            RuleFor(x => x.EstadoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Estado"));
        }

        private bool BeUnique(Municipio entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
