import { BaseModel } from '../base-model';

export class Barragem extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public ativo?: boolean
    ) { super(); }
}
