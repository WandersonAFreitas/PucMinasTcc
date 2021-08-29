import { BaseModel } from '../base-model';

export class Situacao extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public padrao?: boolean,
        public tipoSituacao?: number
    ) { super(); }
}
