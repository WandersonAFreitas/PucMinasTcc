import { ManageUser } from './manage-user';
import { BaseModel } from '../base-model';
export class Arquivo extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public extensao?: string,
        public tipo?: string,
        public hash?: string,
        public dataCriacao?: Date,
        public userId?: number,
        public user?: ManageUser,
    ) { super(); }
}
