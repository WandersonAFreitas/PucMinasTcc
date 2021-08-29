import { BaseModel } from "../base-model";

export class Pais extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public sigla?: string
    ) { super(); }
}
