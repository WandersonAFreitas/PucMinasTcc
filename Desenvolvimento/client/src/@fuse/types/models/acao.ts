import { BaseModel } from "../base-model";

export class Acao extends BaseModel {
    constructor(
        public id: number,
        public nome?: string
    ) { super(); }
}
