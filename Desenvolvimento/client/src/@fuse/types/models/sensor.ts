import { BaseModel } from '../base-model';

export class Sensor extends BaseModel {
    constructor(
        public id: number,
        public descricao?: string,
        public ativo?: boolean
    ) { super(); }
}
