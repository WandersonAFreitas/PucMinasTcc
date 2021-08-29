import { debug } from 'util';

export class ClientError {

    get userMessages(): string[] {
        return this._userMessages;
    }

    set userMessages(userMessages: string[]) {
        this._userMessages = userMessages;
    }

    get errorCode(): number {
        return this._errorCode;
    }

    set errorCode(errorCode: number) {
        this._errorCode = errorCode;
    }

    get devMessages(): string[] {
        return this._devMessages;
    }

    set devMessages(devMessages: string[]) {
        this._devMessages = devMessages;
    }

    private _userMessages: string[];
    private _devMessages: string[];
    private _errorCode: number;

    public static processError(res): ClientError {
        try {
            if (typeof res.error === 'string') {
                return new ClientError([res.error]);
            }

            if (!res.error.userMessages) {
                return new ClientError(['Ocorreu um erro inesperado']);
            }
            const clientError = <ClientError>res.error;
            return clientError;
        } catch (e) {
            return new ClientError(['Ocorreu um erro inesperado']);
        }
    }

    constructor(userMessage: string[], devMessage?: string[], errorCode?) {
        this._userMessages = userMessage;
        this._devMessages = devMessage;
        this._errorCode = errorCode;
    }

}
