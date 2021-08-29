import { Empresa } from './empresa';
import { BaseModel } from '../base-model';
export class Setor extends BaseModel {
    constructor(
        public id: number,
        public sigla?: string,
        public nome?: string,
        public ativo?: boolean,
        public mesmoEnderecoDaEmpresa?: boolean,
        public empresaId?: number,
        public empresa?: Empresa,
        public setorPaiId?: number,
        public setorPai?: Setor,
        public setoresFilhos?: Array<Setor>,
    ) { super(); }
}
