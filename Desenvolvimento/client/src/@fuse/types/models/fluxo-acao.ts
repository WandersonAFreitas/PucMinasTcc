import { Fluxo } from './fluxo';
import { BaseModel } from '../base-model';

export class FluxoAcao extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public fluxoId?: number,
        public fluxo?: Fluxo
    ) { super(); }
}
