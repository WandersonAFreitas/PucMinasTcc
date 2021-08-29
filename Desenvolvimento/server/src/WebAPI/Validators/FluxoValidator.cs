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
    public class FluxoValidator : AbstractValidator<Fluxo>
    {
        private readonly IFluxoService _service;
        private readonly IFluxoItemService _serviceFluxoItem;
        private readonly IFluxoSituacaoService _serviceFluxoSituacao;
        private readonly IParametroService _serviceParametro;

        public FluxoValidator(
            IFluxoService service,
            IFluxoItemService serviceFluxoItem,
            IFluxoSituacaoService serviceFluxoSituacao,
            IParametroService serviceParametro)
        {
            _service = service;
            _serviceFluxoItem = serviceFluxoItem;
            _serviceFluxoSituacao = serviceFluxoSituacao;
            _serviceParametro = serviceParametro;

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Descrição"))
                .MinimumLength(1).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 1))
                .MaximumLength(1000).WithMessage(string.Format(Resources.MaximumLength, "Descrição", 1000))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Fluxo"));

            RuleFor(x => x.Observacao)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Observação"));

            RuleFor(x => x)
                .Custom((entity, context) =>
                {
                    if (entity.Ativo && entity.TramitarEm == (int)TramitarEmEnum.FluxoDefinido)
                    {
                        var query = _serviceFluxoItem.GetQueryableAsync().Result;
                        var exist = query.Any(x => x.FluxoId == entity.Id);

                        if (!exist)
                            context.AddFailure("Não existe configuração de fluxo, não é permitido ativar.");

                        var queryParametro = _serviceParametro.GetQueryableAsync().Result;
                        var findParametro = queryParametro.Where(x => x.Nome == ParametroEnum.SituacaoFinal.Descricao()).FirstOrDefault();

                        var querySituacao = _serviceFluxoSituacao.GetQueryableAsync().Result;
                        var findSituacao = querySituacao.Where(x => x.FluxoId == entity.Id &&
                                                                    x.Nome == findParametro.Valor).FirstOrDefault();

                        if (findSituacao != null)
                        {
                            exist = query.Any(x => x.FluxoId == entity.Id &&
                                                   x.ProximaSituacaoId == findSituacao.Id);
                            if (!exist)
                                context.AddFailure(string.Format("Não existe situação ''{0}'' configurada no fluxo.", findParametro.Valor));
                        }
                    }
                });
        }

        private bool BeUnique(Fluxo entity, string descricao)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Descricao == entity.Descricao && x.Id != entity.Id);
            return !exist;
        }
    }
}
