import { Setor } from './setor';
import { BaseModel } from '../base-model';
export class Empresa extends BaseModel {
    constructor(
        public id: number,
        public sigla?: string,
        public nome?: string,
        public ativo?: boolean,
        public setores?: Array<Setor>,
    ) { super(); }
}