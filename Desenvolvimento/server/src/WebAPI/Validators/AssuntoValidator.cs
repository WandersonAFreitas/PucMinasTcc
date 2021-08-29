using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using System.Linq;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class AssuntoValidator : AbstractValidator<Assunto>
    {
        private readonly IAssuntoService _service;

        public AssuntoValidator(IAssuntoService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(NomeBeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Assunto"));
        }

        private bool NomeBeUnique(Assunto entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
