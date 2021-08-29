import { BaseModel } from '../base-model';

export class User extends BaseModel {
    constructor(
        public id: number,
        public userName?: string,
        public email?: string
    ) { super(); }
}
