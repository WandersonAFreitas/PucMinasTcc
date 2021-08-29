import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { finalize, catchError, map, tap } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { ShellService } from './shell.service';
import * as strings from './../../assets/strings';
import { CredentialsService } from './credentials.service';
import { ClientError } from '@fuse/types/models/viewmodel/client-error.viewmodel';

@Injectable()
export class HttpBaseService {

    private apiUrl = '';
    private body: Object = {};
    private headers: HttpHeaders;

    constructor(
        private http: HttpClient,
        private credentialsService: CredentialsService,
        // private modalLogoffService: ModalLogoffService,
        private shellService: ShellService
    ) {
        this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        this.apiUrl = environment.apiURL;
    }

    getDefault(url: string, options: any, blockUI = true) {
        this.blockUI(blockUI);
        return this.http.get(url, options)
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                catchError((res: HttpErrorResponse) => {
                    return this.formatError(res);
                })
            );
    }

    getFile(url: string, blockUI = true): Observable<Blob> {
        this.blockUI(blockUI);
        this.appendTokenToRequest();

        const options: {
            headers?: HttpHeaders,
            observe?: 'body',
            params?: HttpParams,
            reportProgress?: boolean,
            responseType: 'json',
            withCredentials?: boolean
        } = {
            headers: this.headers,
            responseType: 'blob' as 'json'
        };

        return this.http.get<Blob>(this.apiUrl + url, options)
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                catchError((res: Response) => {
                    const reader = new FileReader();
                    reader.addEventListener('loadend', (e) => {
                        const error = JSON.parse(e.srcElement['result']);
                        res['error'] = error;
                        return this.formatError(res);
                    });
                    reader.readAsText(res['error']);
                    return this.formatError(res);
                })
            );
    }

    postFile(url: string, blockUI = true, body?: any): Observable<Blob> {
        this.blockUI(blockUI);
        this.appendTokenToRequest();
        if (body !== undefined) {
            this.body = JSON.stringify(body);
        }
        const options: {
            headers?: HttpHeaders,
            observe?: 'body',
            params?: HttpParams,
            reportProgress?: boolean,
            responseType: 'json',
            withCredentials?: boolean
        } = {
            headers: this.headers,
            responseType: 'blob' as 'json'
        };

        return this.http.post<Blob>(this.apiUrl + url, this.body, options)
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                catchError((res: Response) => {
                    const reader = new FileReader();
                    reader.addEventListener('loadend', (e) => {
                        const error = JSON.parse(e.srcElement['result']);
                        res['error'] = error;
                        return this.formatError(res);
                    });
                    reader.readAsText(res['error']);
                    return this.formatError(res);
                })
            );
    }

    get(url: string, blockUI = true) {
        this.blockUI(blockUI);
        this.appendTokenToRequest();
        return this.http.get(this.apiUrl + url, this.getRequestOptions())
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                map((res) => this.refreshToken(res)),
                catchError((res: Response) => this.formatError(res))
            );
    }

    postDefault(url: string, options: any, body?: any, blockUI = true) {
        this.blockUI(blockUI);
        return this.http.post(url, body, options)
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                catchError((res: Response) => {
                    return this.formatError(res);
                })
            );
    }

    post(url: string, blockUI = true, body?: any) {
        this.blockUI(blockUI);
        this.appendTokenToRequest();
        if (body !== undefined) {
            this.body = JSON.stringify(body);
        }
        return this.http.post(this.apiUrl + url, this.body, this.getRequestOptions())
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                map((res) => this.refreshToken(res)),
                catchError((res: Response) => this.formatError(res))
            );
    }

    put(url: string, blockUI = true, body?: any) {
        this.blockUI(blockUI);
        this.appendTokenToRequest();
        if (body !== undefined) {
            this.body = JSON.stringify(body);
        }
        return this.http.put(this.apiUrl + url, this.body, this.getRequestOptions())
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                map((res) => this.refreshToken(res)),
                catchError((res: Response) => this.formatError(res))
            );
    }

    delete(url: string, blockUI = true, body?: any) {
        this.blockUI(blockUI);
        this.appendTokenToRequest();
        if (body !== undefined) {
            this.body = JSON.stringify(body);
        }
        return this.http.delete(this.apiUrl + url, this.getRequestOptions())
            .pipe(
                finalize(() => {
                    this.unblockUI(blockUI);
                }),
                map((res) => this.refreshToken(res)),
                catchError((res: Response) => this.formatError(res))
            );
    }

    formatError(res) {
        if (res.headers != null && res.headers.has('WWW-Authenticate')) {
            // this.modalLogoffService.expirarSessao();
            return;
        }
        const erro: ClientError = ClientError.processError(res);

        if (erro && (!erro.errorCode || (erro.errorCode >= 400 && erro.errorCode <= 499))) {
            this.shellService.alert().warning({ timeout: 10000, messages: erro.userMessages });
        } else {
            this.shellService.alert().error({ timeout: 100000, messages: erro.userMessages });
        }

        erro.devMessages ? console.warn(erro.devMessages, erro.errorCode) : console.warn(erro.userMessages, erro.errorCode);
        return throwError(res);
    }

    refreshToken(res) {
        // this.credentialsService.refreshToken(res.headers.get(strings.tokenHeader));
        const contentType = res.headers.get('Content-type');
        if (!contentType) {
            return null;
        }
        if (contentType.includes('json')) {
            return res.body;
        } else if (contentType.includes('text')) {
            return res.text();
        }
    }

    appendTokenToRequest() {
        if (this.credentialsService.getToken()) {
            this.headers = this.headers.set(strings.tokenHeader, strings.prefixTokenHeader + this.credentialsService.getToken());
        }
    }

    private getRequestOptions(): Object {
        return { headers: this.headers, observe: 'response' };
    }


    private blockUI(blockUI: boolean) {
        setTimeout(() => {
            if (blockUI) {
                this.shellService.loader().open();
            }
        }, 0);
    }

    private unblockUI(blockUI: boolean) {
        setTimeout(() => {
            if (blockUI) {
                this.shellService.loader().close();
            }
        }, 10);
    }
}
