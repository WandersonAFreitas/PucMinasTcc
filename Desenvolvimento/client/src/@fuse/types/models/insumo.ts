import { BaseModel } from '../base-model';
import { User } from './user';
import { UnidadeMedida } from './unidade-medida';
import { Marca } from './marca';
import { Setor } from './setor';
import { TipoInsumo } from './tipo-insumo';
import { Fornecedor } from './fornecedor';

export class Insumo extends BaseModel {
    constructor(
        public id: number,
        public identificador?: string,
        public nome?: string,
        public descricao?: string,
        public observacao?: string,
        public modelo?: string,
        public patrimonio?: string,
        public ativo?: boolean,
        public dataCriacao?: Date,
        public dataInativacao?: Date,

        public criadoPorId?: number,
        public criadoPor?: User,

        public alteradoPorId?: number,
        public alteradoPor?: User,

        public unidadeMedidaId?: number,
        public unidadeMedida?: UnidadeMedida,

        public marcaId?: number,
        public marca?: Marca,

        public tipoInsumoId?: number,
        public tipoInsumo?: TipoInsumo,

        public setorId?: number,
        public setor?: Setor,

        public fornecedorId?: number,
        public fornecedor?: Fornecedor
    ) { super(); }
}
