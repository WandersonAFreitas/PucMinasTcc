import { Fluxo } from './fluxo';
import { BaseModel } from '../base-model';

export class FluxoSituacao extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public padrao?: boolean,
        public tipoSituacao?: number,
        public fluxoId?: number,
        public fluxo?: Fluxo
    ) { super(); }
}
