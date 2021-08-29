import { Estado } from './estado';
import { BaseModel } from '../base-model';

export class Municipio extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public estadoId?: number,
        public estado?: Estado 
    ) { super(); }
}
