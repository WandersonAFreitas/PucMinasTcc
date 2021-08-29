using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using System.Linq;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        private readonly IEnderecoService _service;

        public EnderecoValidator(IEnderecoService service)
        {
            _service = service;

            RuleFor(x => x.CEP)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "CEP"))
                .MaximumLength(15).WithMessage(string.Format(Resources.MaximumLength, "CEP", 15));

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Logradouro"))
                .MaximumLength(200).WithMessage(string.Format(Resources.MaximumLength, "Logradouro", 200));

            RuleFor(x => x.Complemento)
                .MaximumLength(200).WithMessage(string.Format(Resources.MaximumLength, "Complemento", 200));

            RuleFor(x => x.Bairro)
                .MaximumLength(200).WithMessage(string.Format(Resources.MaximumLength, "Bairro", 200));

            RuleFor(x => x.Bairro)
                .MaximumLength(20).WithMessage(string.Format(Resources.MaximumLength, "Número", 20));
        }
    }
}
