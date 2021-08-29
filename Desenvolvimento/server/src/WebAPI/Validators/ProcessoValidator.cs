using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class ProcessoValidator : AbstractValidator<Processo>
    {
        private readonly IProcessoService _processoService;
        private readonly IEmpresaService _empresaService;
        private readonly IAssuntoService _asssuntoService;

        public ProcessoValidator(
            IProcessoService processoService,
            IEmpresaService empresaService,
            IAssuntoService asssuntoService
            )
        {
            _processoService = processoService;
            _empresaService = empresaService;
            _asssuntoService = asssuntoService;

            RuleFor(x => x.EmpresaId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Empresa"));

            RuleFor(x => x.AssuntoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Assunto"));

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Descrição"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 1))
                .MaximumLength(1000).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 1000));
        }
    }
}
