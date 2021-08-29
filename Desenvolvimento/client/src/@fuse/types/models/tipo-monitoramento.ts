import { BaseModel } from '../base-model';

export class TipoMonitoramento extends BaseModel {
    constructor(
        public id: number,
        public nome?: string,
        public ativo?: boolean
    ) { super(); }
}
