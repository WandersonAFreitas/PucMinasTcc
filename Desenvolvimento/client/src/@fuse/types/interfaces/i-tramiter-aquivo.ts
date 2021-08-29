export interface ITramiteArquivoViewModel {
    id: number;
    nome: string;
    hash: string;
    tipo: string;
    tramiteId: number;
    arquivoId: number;
    fluxoItemTipoAnexoId: number;
    exigeAssinaturaDigital: boolean;
    obrigatorio: boolean;
}