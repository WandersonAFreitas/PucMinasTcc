import { Fluxo } from './fluxo';
import { FluxoSituacao } from './fluxo-situacao';
import { FluxoAcao } from './fluxo-acao';
import { FluxoItemCheckList } from './fluxo-item-checklist';
import { FluxoItemSetor } from './fluxo-item-setor';
import { FluxoItemAnexo } from './fluxo-item-anexo';
import { BaseModel } from '../base-model';

export class FluxoItem extends BaseModel {
    constructor(
        public id: number,
        public fluxoId?: number,
        public fluxo?: Fluxo,
        public situacaoAtualId?: number,
        public situacaoAtual?: FluxoSituacao,
        public acaoId?: number,
        public acao?: FluxoAcao,
        public proximaSituacaoId?: number,
        public proximaSituacao?: FluxoSituacao,
        public fluxoItemSetores?: Array<FluxoItemSetor>,
        public fluxoItemTiposAnexo?: Array<FluxoItemAnexo>,
        public fluxoItemChecklists?: Array<FluxoItemCheckList>
    ) { super(); }
}
