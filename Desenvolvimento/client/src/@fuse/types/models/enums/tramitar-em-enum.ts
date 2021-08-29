export class TramitarEm {
    constructor(
        public key: any,
        public value: string
    ) { }
}

export enum TramitarEmEnum {
    SetoresDaEmpresaDoProcesso = 1,
    EntreSetoresDeTodasAsEmpresa = 2,
    FluxoDefinido = 3
}

export const TramitarEmEnumArrayKeyValue = [
    new TramitarEm('', 'Todos'),
    new TramitarEm(TramitarEmEnum.SetoresDaEmpresaDoProcesso, 'Setores da empresa do processo'),
    new TramitarEm(TramitarEmEnum.EntreSetoresDeTodasAsEmpresa, 'Entre setores de todas as empresas'),
    new TramitarEm(TramitarEmEnum.FluxoDefinido, 'Fluxo definido'),
];
