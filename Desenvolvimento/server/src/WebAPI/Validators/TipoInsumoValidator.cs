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
    public class TipoInsumoalidator : AbstractValidator<TipoInsumo>
    {
        private readonly ITipoInsumoService _service;

        public TipoInsumoalidator(ITipoInsumoService service)
        {
            _service = service;

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Descrição"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "A descrição de tipo de insumo"));
        }

        private bool BeUnique(TipoInsumo entity, string descricao)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Descricao == entity.Descricao && x.Id != entity.Id);
            return !exist;
        }
    }
}
