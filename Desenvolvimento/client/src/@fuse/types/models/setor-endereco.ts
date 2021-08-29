import { Endereco } from './endereco';
import { Setor } from './setor';
import { BaseModel } from '../base-model';

export class SetorEndereco extends BaseModel {
    constructor(
        public id: number,
        public setorId?: number,
        public setor?: Setor,
        public enderecoId?: number,
        public endereco?: Endereco
        ) { super(); }
}
