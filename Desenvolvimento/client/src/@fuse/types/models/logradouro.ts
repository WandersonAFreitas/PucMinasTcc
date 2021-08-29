import { Pais } from './pais';
import { Estado } from './estado';
import { Municipio } from './municipio';
import { BaseModel } from '../base-model';

export class Logradouro extends BaseModel {
    constructor(
        public id: number,
        public cep?: string,
        public endereco?: string,
        public bairro?: string,
        
        public paisId?: number,
        public pais?: Pais, 
        
        public estadoId?: number,
        public estado?: Estado,

        public municipioId?: number,
        public municipio?: Municipio 
    ) { super(); }
}
