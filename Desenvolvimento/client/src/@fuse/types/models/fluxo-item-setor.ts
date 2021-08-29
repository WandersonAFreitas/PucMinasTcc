import { Setor } from './setor';
import { FluxoItem } from './fluxo-item';
import { BaseModel } from '../base-model';

export class FluxoItemSetor extends BaseModel {
    constructor(
        public id: number,
        public setorId?: number,
        public setor?: Setor,
        public fluxoItemId?: number,
        public fluxoItem?: FluxoItem
    ) { super(); }
}
