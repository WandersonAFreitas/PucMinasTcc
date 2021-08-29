import { Empresa } from './empresa';
import { Setor } from './setor';
import { BaseModel } from '../base-model';
import { AssuntoArquivo } from './assunto-arquivo';
import { Fluxo } from './fluxo';

export class Assunto extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public ativo?: boolean,
        public hashArquivoModelo?: string,
        public orientacoes?: string,

        public empresaId?: number,
        public empresa?: Empresa,

        public setorProcessoFisicoId?: number,
        public setorProcessoFisico?: Setor,

        public setorProcessoVirtualId?: number,
        public setorProcessoVirtual?: Setor,

        public fluxoId?: number,
        public fluxo?: Fluxo,

        public assuntoArquivos?: AssuntoArquivo[],
    ) { super(); }
}
