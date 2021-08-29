import { Processo } from './processo';
import { Autor } from './autor';
import { BaseModel } from '../base-model';

export class ProcessoAutor extends BaseModel {
    constructor(
        public id: number,
        public processoId?: number,
        public autorId?: number,
        public autor?: Autor,
        public processo?: Processo,
    ) { super(); }
}
