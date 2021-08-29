import { ManageUserSetor } from './manage-user-setor';
import { BaseModel } from '../base-model';
export class ManageUser extends BaseModel {
    constructor(
        public id: number,
        public userName?: string,
        public email?: string,
        public roles?: ManagerRole[],
        public rolesSting?: string,
        public userSetores?: ManageUserSetor[],
        public userSetoresString?: string
    ) { super(); }
}

export class ManagerRole extends BaseModel {
    constructor(
        public id: number,
        public name?: string,
        public enabled?: boolean
    ) { super(); }
}

export interface IManageUser {
    id: number;
    userName: string;
    email: string;
}


