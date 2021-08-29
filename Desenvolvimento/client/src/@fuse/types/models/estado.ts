import { Pais } from './pais';
import { BaseModel } from '../base-model';

export class Estado extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public sigla?: string,
        public paisId?: number,
        public pais?: Pais
    ) { super(); }
}
