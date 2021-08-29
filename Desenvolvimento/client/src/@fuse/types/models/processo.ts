import { Empresa } from './empresa';
import { FluxoSituacao } from './fluxo-situacao';
import { Assunto } from './assunto';
import { Setor } from './setor';
import { Tramite } from './tramite';
import { ProcessoAutor } from './processo-autor';
import { BaseModel } from '../base-model';
import { User } from './user';

export class Processo extends BaseModel {
    constructor(
        public id: number,
        public sequencial?: number,
        public ano?: number,
        public empresaId?: number,
        public assuntoId?: number,
        public situacaoId?: number,
        public descricao?: string,
        public responsavelId?: number,
        public ultimaAltercao?: Date,
        public criacao?: Date,
        public setorId?: number,
        public empresa?: Empresa,
        public assunto?: Assunto,
        public situacao?: FluxoSituacao,
        public responsavel?: User,
        public setor?: Setor,
        public tramites?: Array<Tramite>,
        public processoAutores?: Array<ProcessoAutor>,
    ) { super(); }
}
