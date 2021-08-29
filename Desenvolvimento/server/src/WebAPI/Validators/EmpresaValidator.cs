using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using System.Linq;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        private readonly IEmpresaService _service;

        public EmpresaValidator(IEmpresaService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(NomeBeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Nome da Empresa"));

            RuleFor(x => x.Sigla)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Sigla"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 1))
                .MaximumLength(100).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 100))
                .Must(SiglaBeUnique).WithMessage(string.Format(Resources.MustBeUnique, "A Sigla da Empresa"));
        }

        private bool NomeBeUnique(Empresa entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }

        private bool SiglaBeUnique(Empresa entity, string sigla)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Sigla == entity.Sigla && x.Id != entity.Id);
            return !exist;
        }
    }
}
