import { Municipio } from './municipio';
import { SetorEndereco } from './setor-endereco';
import { EmpresaEndereco } from './empresa-endereco';
import { BaseModel } from '../base-model';

export class Endereco extends BaseModel {
    constructor(
        public id: number,
        public cep?: string,
        public logradouro?: string,
        public complemento?: string,
        public bairro?: string,
        public numero?: string,
        public municipioId?: number,
        public municipio?: Municipio, 
        public empresaEnderecos?: Array<EmpresaEndereco>,
        public setorEnderecos?: Array<SetorEndereco>
        ) { super(); }
}
