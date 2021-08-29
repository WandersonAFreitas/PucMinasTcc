import { BaseModel } from '../base-model';

export class Consultoria extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
    ) { super(); }
}
