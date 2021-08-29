import { Assunto } from './assunto';
import { FluxoSituacao } from './fluxo-situacao';
import { FluxoAcao } from './fluxo-acao';
import { FluxoTipoAnexo } from './fluxo-tipo-anexo';
import { FluxoItem } from './fluxo-item';
import { BaseModel } from '../base-model';

export class Fluxo extends BaseModel {
    constructor(
        public id: number,
        public descricao?: string,
        public observacao?: string,
        public ativo?: boolean,
        public tramitarEm?: number,
        public assuntos?: Array<Assunto>,
        public situacoes?: Array<FluxoSituacao>,
        public acoes?: Array<FluxoAcao>,
        public tiposAnexo?: Array<FluxoTipoAnexo>,
        public fluxoItems?: Array<FluxoItem>
    ) { super(); }
}
