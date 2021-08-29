import { BaseModel } from '../base-model';

export class Fornecedor extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
    ) { super(); }
}
