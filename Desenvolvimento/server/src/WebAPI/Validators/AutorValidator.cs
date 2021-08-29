using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        private readonly IAutorService _autorService;

        public AutorValidator(
            IAutorService autorService
            )
        {
            _autorService = autorService;


            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Nome", 1))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUniqueNome).WithMessage(string.Format(Resources.MustBeUnique, "O Nome do Autor"));
         
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(string.Format(Resources.Invalid, "E-mail"))
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "E-mail"))
                .Must(BeUniqueEmail).WithMessage(string.Format(Resources.MustBeUnique, "O E-mail"));

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Data de Nascimento"));

            RuleFor(x => x.CpfCnpj)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "CPF/CNPJ"))
                .Must(BeUniqueCpf).WithMessage(string.Format(Resources.MustBeUnique, "O CPF/CNPJ"));

            RuleFor(x => x)
                .Custom((entity, context) =>
                {
                    if (!string.IsNullOrEmpty(entity.CpfCnpj))
                    {
                        var cpf = string.Concat(entity.CpfCnpj.Where(char.IsDigit));

                        if(cpf.Length != 11)
                        {
                            context.AddFailure(string.Format(Resources.Invalid, "CPF/CNPJ"));
                        }
                    }
                });
        }

        private bool BeUniqueNome(Autor entity, string nome)
        {
            var query = _autorService.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }

        private bool BeUniqueCpf(Autor entity, string cpf)
        {
            var query = _autorService.GetQueryableAsync().Result;
            var exist = query.Any(x => x.CpfCnpj == entity.CpfCnpj && x.Id != entity.Id);
            return !exist;
        }
        private bool BeUniqueEmail(Autor entity, string email)
        {
            var query = _autorService.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Email == entity.Email && x.Id != entity.Id);
            return !exist;
        }
    }
}
