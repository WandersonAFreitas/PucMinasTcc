import { Empresa } from './empresa';
import { Endereco } from './endereco';
import { BaseModel } from '../base-model';

export class EmpresaEndereco extends BaseModel {
    constructor(
        public id: number,
        public empresaId?: number,
        public empresa?: Empresa,
        public enderecoId?: number,
        public Eendereco?: Endereco
        ) { super(); }
}
