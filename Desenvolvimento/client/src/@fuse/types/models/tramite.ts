import { Setor } from './setor';
import { User } from './user';
import { Processo } from './processo';
import { TramiteArquivo } from './tramite-arquivo';
import { TramiteChecklist } from './tramite-check-list';
import { FluxoAcao } from './fluxo-acao';
import { FluxoSituacao } from './fluxo-situacao';
import { BaseModel } from '../base-model';

export class Tramite extends BaseModel {
    constructor(
        public id: number,
        public tramitado?: boolean,
        public observacao?: string,
        public dataTramite?: Date,
        public enviarEmailAutores?: boolean,
        public enviarEmailsPara?: string,
        public acaoId?: number,
        public setorId?: number,
        public processoId?: number,
        public situacaoId?: number,
        public situacaoDoProcessoNoTramiteId?: number,
        public responsavelId?: number,
        public acao?: FluxoAcao,
        public setor?: Setor,
        public processo?: Processo,
        public situacao?: FluxoSituacao,
        public situacaoDoProcessoNoTramite?: FluxoSituacao,
        public responsavel?: User,
        public tramiteArquivos?: Array<TramiteArquivo>,
        public tramiteChecklists?: Array<TramiteChecklist>,
    ) { super(); }
}
