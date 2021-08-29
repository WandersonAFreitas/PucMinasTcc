import { BaseModel } from '../base-model';

export class TipoAnexo extends BaseModel {
    constructor(
        public id: number,
        public nome?: string
    ) { super(); }
}
