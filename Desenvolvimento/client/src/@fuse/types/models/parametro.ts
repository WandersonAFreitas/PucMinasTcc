import { BaseModel } from "../base-model";

export class Parametro extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public valor?: string,
        public tipo?: string,
    ) { super(); }
}
