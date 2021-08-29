import { Assunto } from './assunto';
import { BaseModel } from '../base-model';
import { Arquivo } from './arquivo';
export class AssuntoArquivo extends BaseModel {
    constructor(
        public id: number,
        public assuntoId?: number,
        public assunto?: Assunto,
        public arquivoId?: number,
        public arquivo?: Arquivo,
    ) { super(); }
}
