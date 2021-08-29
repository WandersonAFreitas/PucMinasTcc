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
    public class ParametroValidator : AbstractValidator<Parametro>
    {
        private readonly IParametroService _service;

        public ParametroValidator(IParametroService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Nome", 1))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Nome do Parametro"));

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Valor"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Valor", 1));
        }

        private bool BeUnique(Parametro entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
