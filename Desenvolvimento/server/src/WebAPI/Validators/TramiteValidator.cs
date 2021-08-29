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
    public class TramiteValidator : AbstractValidator<Tramite>
    {
        private readonly ITramiteService _tramiteService;
        private readonly IProcessoService _processoService;

        public TramiteValidator(
            ITramiteService tramiteService,
            IProcessoService processoService
            )
        {
            _tramiteService = tramiteService;
            _processoService = processoService;

            RuleFor(x => x.AcaoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Ação"));

            RuleFor(x => x.SetorId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Setor"));

            //RuleFor(x => x.SituacaoId)
            //    .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Situação"));

            RuleFor(x => x.Observacao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Observação"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Observação", 1))
                .MaximumLength(1000).WithMessage(string.Format(Resources.MaximumLength, "Observação", 1000));

            RuleFor(x => x.ProcessoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Processo"));

            RuleFor(x => x)
                .Custom((entity, context) =>
                {
                    var queryProcesso = _processoService.GetQueryableAsync().Result;
                    var processo = queryProcesso
                    .Where(x => x.Id == entity.ProcessoId)
                    .Include(x => x.Responsavel)
                    .Include(x => x.Assunto)
                    .ThenInclude(x => x.Fluxo)
                    .FirstOrDefault();

                    if (entity.SituacaoId == 0 && processo != null && processo.Assunto == null &&  processo.Assunto.Fluxo == null && processo.Assunto.Fluxo.TramitarEm != (int)TramitarEmEnum.FluxoDefinido)
                    {
                        context.AddFailure(string.Format(Resources.NotEmpty, "Situação"));
                    }

                    if (entity.Tramitado && processo != null && processo.ResponsavelId == null)
                    {
                        context.AddFailure("O Processo não pode ser tramitado, pois não há responsável.");
                    }
                });
        }
    }
}
