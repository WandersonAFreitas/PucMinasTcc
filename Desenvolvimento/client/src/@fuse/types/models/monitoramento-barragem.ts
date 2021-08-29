import { BaseModel } from '../base-model';
import { Barragem } from './barragem';
import { NivelMonitoramento } from './nivel-monitoramento';
import { UnidadeMedida } from './unidade-medida';
import { Sensor } from './sensor';
import { Consultoria } from './Consultoria';
import { TipoMonitoramento } from "./tipo-monitoramento";

export class MonitoramentoBarragem extends BaseModel {
    constructor(
        public id: number,
        public descricao?: string,
        public observacao?: string,
        
        public barragemId?: number,
        public barragem?: Barragem,
        
        public nivelMonitoramentoId?: number,
        public nivelMonitoramento?: NivelMonitoramento,

        public nivel?: number,
        
        public unidadeMedidaId?: number,
        public unidadeMedida?: UnidadeMedida,
        
        public dataHora?: Date,
        public latitude?: string,
        public longitude?: string,

        public sensorId?: number,
        public sensor?: Sensor,

        public consultoriaId?: number,
        public consultoria?: Consultoria,

        public tipoMonitoramentoId?: number,
        public tipoMonitoramento?: TipoMonitoramento

    ) { super(); }
}
