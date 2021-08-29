import { FluxoItemCheckList } from './fluxo-item-checklist';
import { BaseModel } from '../base-model';
import { Tramite } from './tramite';

export class TramiteChecklist extends BaseModel {
    constructor(
        public id: number,
        public checado: boolean,
        public tramiteId?: number,
        public fluxoItemChecklistId?: number,
        public tramite?: Tramite,
        public fluxoItemChecklist?: FluxoItemCheckList ,
    ) { super(); }
}
