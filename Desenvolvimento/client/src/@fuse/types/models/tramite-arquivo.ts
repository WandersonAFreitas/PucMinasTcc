import { Tramite } from './tramite';
import { Arquivo } from './arquivo';
import { FluxoItemAnexo } from './fluxo-item-anexo';
import { BaseModel } from '../base-model';

export class TramiteArquivo extends BaseModel {
    constructor(
        public id: number,
        public tramiteId?: number,
        public arquivoId?: number,
        public fluxoItemTipoAnexoId?: number,
        public tramite?: Tramite,
        public arquivo?: Arquivo,
        public fluxoItemTipoAnexo?: FluxoItemAnexo,
    ) { super(); }
}
