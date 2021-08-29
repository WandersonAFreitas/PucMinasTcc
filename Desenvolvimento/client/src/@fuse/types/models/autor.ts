import { ProcessoAutor } from './processo-autor';
import { BaseModel } from '../base-model';

export class Autor extends BaseModel {
    constructor(
        public id: number,
        public cpfCnpj?: string,
        public nome?: string,
        public dataNascimento?: Date,
        public email?: string,
        public processoAutores?: Array<ProcessoAutor>,
    ) { super(); }
}
