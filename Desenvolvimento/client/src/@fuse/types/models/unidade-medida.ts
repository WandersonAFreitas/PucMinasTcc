import { BaseModel } from '../base-model';

export class UnidadeMedida extends BaseModel {
    constructor(
        public id: number,
        public descricao?: string
    ) { super(); }
}
