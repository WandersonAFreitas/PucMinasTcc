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
    public class InsumoValidator : AbstractValidator<Insumo>
    {
        private readonly IInsumoService _service;

        public InsumoValidator(IInsumoService service)
        {
            _service = service;

            RuleFor(x => x.TipoInsumoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Tipo de insumo"));

            RuleFor(x => x.Identificador)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Identificador"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Identificador", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Identificador", 300))
                .Must(BeUniqueIdentificador).WithMessage(string.Format(Resources.MustBeUnique, "O identificador"));
            
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUniqueNome).WithMessage(string.Format(Resources.MustBeUnique, "O nome"));

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Descrição"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 300));

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Modelo"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Modelo", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Modelo", 300));

            RuleFor(x => x.Patrimonio)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Patrimonio"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Patrimonio", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Patrimonio", 300));
        }

        private bool BeUniqueIdentificador(Insumo entity, string identificador)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Identificador == entity.Identificador && x.Id != entity.Id);
            return !exist;
        }

        private bool BeUniqueNome(Insumo entity, string Nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
