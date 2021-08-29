import { FluxoItem } from './fluxo-item';
import { FluxoTipoAnexo } from './fluxo-tipo-anexo';
import { BaseModel } from '../base-model';

export class FluxoItemAnexo extends BaseModel {
    constructor(
        public id: number,
        public obrigatorio?: boolean,
        public exigeAssinaturaDigital?: boolean,

        public fluxoItemId?: number,
        public fluxoItem?: FluxoItem,

        public fluxoTipoAnexoId?: number,
        public fluxoTipoAnexo?: FluxoTipoAnexo
    ) { super(); }
}
