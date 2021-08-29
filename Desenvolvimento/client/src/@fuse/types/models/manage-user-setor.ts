import { BaseModel } from '../base-model';

export class ManageUserSetor extends BaseModel {
    constructor(
        public id: number,
        public userId?: number,
        public setorId?: number,
        public setorPaiId?: number,
        public enabled?: boolean
    ) { super(); }
}
