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
    public class FluxoItemValidator : AbstractValidator<FluxoItem>
    {
        private readonly IFluxoItemService _service;
        private readonly IFluxoSituacaoService _serviceSituacao;
        private readonly IParametroService _serviceParametro;

        public FluxoItemValidator(
            IFluxoItemService service,
            IFluxoSituacaoService serviceSituacao,
            IParametroService serviceParametro)
        {
            _service = service;
            _serviceSituacao = serviceSituacao;
            _serviceParametro = serviceParametro;

            RuleFor(x => x.SituacaoAtualId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Situação Inicial"))
                .Must(BeSituacaoAtual).WithMessage(string.Format(Resources.MustNotBeAllowed, "Situação atual"));

            RuleFor(x => x.AcaoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Ação"));
                //.Must(BeUniqueAcao).WithMessage($"Ação já cadastrada.");

            RuleFor(x => x.ProximaSituacaoId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Próxima Situação"))
                .Must(BeSituacaoAtual).WithMessage(string.Format(Resources.MustNotBeAllowed, "Próxima situação"));
                
            RuleFor(x => x)
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Fluxo"))
                .Custom((entity, context) => {

                    var query = _service.GetQueryableAsync().Result;
                    var exist = query.Any(x => x.FluxoId == entity.FluxoId);

                    if (!exist)
                    {
                        var queryParametro = _serviceParametro.GetQueryableAsync().Result;
                        var findParametro = queryParametro.Where(x => x.Nome == ParametroEnum.SituacaoInicial.Descricao()).FirstOrDefault();

                        var querySituacao = _serviceSituacao.GetQueryableAsync().Result;
                        var findSituacao = querySituacao.Where(x => x.Id == entity.SituacaoAtualId).FirstOrDefault();

                        if (findSituacao.Nome != findParametro.Valor)
                            context.AddFailure(string.Format(Resources.MustBeThisValue, "A situação atual", findParametro.Valor));
                    }
                });
        }

        private bool BeUnique(FluxoItem entity)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.SituacaoAtualId == entity.SituacaoAtualId && 
                                       x.AcaoId == entity.AcaoId &&
                                       x.ProximaSituacaoId == entity.ProximaSituacaoId &&
                                       x.Id != entity.Id);
            return !exist;
        }

        private bool BeUniqueAcao(FluxoItem entity, long acaoId)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.AcaoId == entity.AcaoId &&
                                       x.FluxoId == entity.FluxoId &&
                                       x.Id != entity.Id);
            return !exist;
        }

        private bool BeSituacaoAtual(FluxoItem entity, long situacaoAtualId)
        {
            var query = _serviceSituacao.GetQueryableAsync().Result;
            var situacao = query.Where(x => x.Id == entity.SituacaoAtualId).FirstOrDefault();

            return (situacao.TipoSituacao == (int)TipoSituacaoEnum.SituacaoAtual) ||
                   (situacao.TipoSituacao == (int)TipoSituacaoEnum.Todas);
        }

        private bool BeProximaSituacao(FluxoItem entity, long proximaSituacaoId)
        {
            var query = _serviceSituacao.GetQueryableAsync().Result;
            var situacao = query.Where(x => x.Id == entity.SituacaoAtualId).FirstOrDefault();

            return (situacao.TipoSituacao == (int)TipoSituacaoEnum.ProximaSituacao) ||
                    (situacao.TipoSituacao == (int)TipoSituacaoEnum.SituacaoFinal) ||
                   (situacao.TipoSituacao == (int)TipoSituacaoEnum.Todas);
        }
    }
}
