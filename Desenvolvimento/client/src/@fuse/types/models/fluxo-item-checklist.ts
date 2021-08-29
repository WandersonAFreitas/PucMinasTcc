import { FluxoItem } from './fluxo-item';
import { BaseModel } from '../base-model';
import { TramiteChecklist } from './tramite-check-list';

export class FluxoItemCheckList  extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public fluxoItemId?: number,
        public fluxoItem?: FluxoItem,
        public tramiteChecklists?: Array<TramiteChecklist>
    ) { super(); }
}
