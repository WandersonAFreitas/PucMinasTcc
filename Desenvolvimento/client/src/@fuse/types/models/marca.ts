import { BaseModel } from '../base-model';

export class Marca extends BaseModel {
    constructor(
        public id: number,
        public descricao?: string,
        public ativo?: boolean
    ) { super(); }
}
