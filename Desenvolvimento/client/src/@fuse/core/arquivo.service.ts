
import { throwError as observableThrowError, Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpBaseService } from './http-base.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map, catchError, finalize } from 'rxjs/operators';
import { ShellService } from './shell.service';
import { CredentialsService } from './credentials.service';
import { IArquivo } from '@fuse/types/interfaces/i-arquivo';
import { ITramiteArquivoViewModel } from '@fuse/types/interfaces/i-tramiter-aquivo';

@Injectable()
export class ArquivoService {

    private apiEndpoint: string;
    private apiController: string;
    private headers: HttpHeaders;
    private options: any;

    constructor(
        private _httpClient: HttpClient,
        private _http: HttpBaseService,
        private _credentialsService: CredentialsService,
        private _shellService: ShellService) {
        this.apiController = '/arquivo';
        this.apiEndpoint = environment.apiURL + this.apiController;
        this.setHeaders();
    }

    private setHeaders() {
        this.headers = new HttpHeaders();
        this.headers = this.headers.set('Authorization', 'Bearer ' + this._credentialsService.getToken());
        this.options = { headers: this.headers };
    }

    public getFile(hash: string): Observable<any> {
        // const hashEncoded = encodeURIComponent(hash);
        const url = `${this.apiEndpoint}/download?hash=${hash}`;
        this.options.observe = 'response';
        this.options.responseType = 'blob';
        this._shellService.blockUI();
        return this._http.getDefault(url, this.options)
            .pipe(
                map((response: any) => {
                    const blob = response.body;
                    const filename = this.getFileName(response);
                    return {
                        data: blob,
                        filename: filename
                    };
                }),
                finalize(() => this._shellService.unblockUI()),
                catchError(error => of(error))
            );
    }

    public getFiles(hash: string[]) {
        const url = `${this.apiEndpoint}/download`;
        const options: any = { headers: this.headers };
        options.observe = 'response';
        options.responseType = 'arraybuffer';
        this._shellService.blockUI();
        return this._httpClient.post(url, hash, options).pipe(map(res => {
            return {
                data: res['body'],
                filename: 'documentos'
            };
        }),
            finalize(() => this._shellService.unblockUI()),
            catchError(error => of(error))
        );
    }

    public downloadFiles(hash: string[]) {
        this.getFiles(hash).subscribe(response => {
            const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/zip' }));
            const a = document.createElement('a');
            document.body.appendChild(a);
            a.href = url;
            a.download = response.filename;
            a.click();
            setTimeout(() => {
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            }, 0);
        },
            error => of(error)
        );
    }

    public downloadFile(hash: string, doDownload: boolean = true) {
        this.getFile(hash).subscribe(response => {
            const a = document.createElement('a');
            document.body.appendChild(a);
            const url = window.URL.createObjectURL(response.data);
            a.href = url;

            if (doDownload) {
                a.download = response.filename;
            } else {
                const extensionsThatCanBeShown = ['application/pdf', 'image/png', 'image/jpeg', 'image/jpg'];
                a.target = '_blank';
                if (!extensionsThatCanBeShown.some(type => type === response.data.type)) {
                    a.download = response.filename;
                }
            }

            a.click();
            setTimeout(() => {
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            }, 0);
        },
            error => of(error));
    }

    public uploadFiles(files: File[], path: string): Observable<any> {
        const formData: FormData = new FormData();
        let uploadPath = 'upload';

        if (path) {
            uploadPath = `${uploadPath}/${path}`;
        }

        files.forEach(file => formData.append('uploadFile', file, file.name));
        if (files.length > 0) {
            this._shellService.blockUI();
            return this._http.postDefault(`${this.apiEndpoint}/${uploadPath}`, this.options, formData)
                .pipe(
                    finalize(() => this._shellService.unblockUI()),
                    catchError((error) => {
                        return observableThrowError(error);
                    })
                );
        }
    }

    public uploadFilesPromise(files: File[], alert = true, path: string): Promise<Array<IArquivo>> {
        return this.uploadFiles(files, path).toPromise().then((response) => {
            alert && this._shellService.alert().success({ messages: ['Upload com sucesso!'], timeout: 2000 });
            return files.map((f, i) => {
                const arquivo: IArquivo = {
                    id: 0,
                    hash: response[i],
                    nome: f.name
                };
                return arquivo;
            }
            );
        });
    }

    public uploadFile(fileList: FileList): Observable<any> {
        if (fileList.length > 0) {
            const file: File = fileList[0];
            const formData: FormData = new FormData();
            formData.append('uploadFile', file, file.name);
            return this._http.postDefault(`${this.apiEndpoint}/upload`, this.options, formData)
                .pipe(
                    map((resposta: any) => resposta),
                    catchError((error) => {
                        return observableThrowError(error);
                    })
                );
        }
    }

    public uploadFilePromise(fileList: FileList): Promise<IArquivo> {
        return this.uploadFile(fileList).toPromise().then((response: Response) => {
            const arquivoSga: IArquivo = {
                id: 0,
                hash: response[0],
                nome: fileList[0].name
            };
            return arquivoSga;
        });
    }

    public remove(id: number, blockUI = true): Observable<IArquivo> {
        return this._http.delete(`${this.apiController}/${id}`, blockUI);
    }

    getFileName(response) {
        const contentDispositionHeader = response.headers.get('Content-Disposition');
        if (!contentDispositionHeader) {
            return 'fail.pdf'
        }
        const result = contentDispositionHeader.split(';')[1].trim().split('=')[1];
        return result.replace(/"/g, '');
    }

    public uploadTramiteArquivosPromise(files: File[], alert = true, tramiteArquivos: ITramiteArquivoViewModel[], tramiteId: number): Promise<Array<IArquivo>> {
        return this.uploadTramiteArquivos(files, tramiteArquivos, tramiteId).toPromise().then((response) => {
            alert && this._shellService.alert().success({ messages: ['Upload com sucesso!'], timeout: 2000 });
            return files.map((f, i) => {
                const arquivo: IArquivo = {
                    id: 0,
                    hash: response[i],
                    nome: f.name
                };
                return arquivo;
            }
            );
        });
    }

    public uploadTramiteArquivos(files: File[], tramiteArquivos: ITramiteArquivoViewModel[], tramiteId: number): Observable<any> {
        const formData: FormData = new FormData();
        const uploadPath = `upload/tramite/${tramiteId}`;

        files.forEach(file => {
            const tramiteArquivo = tramiteArquivos.find(x => x.nome == file.name);
            const tramiteArquivoString = JSON.stringify(tramiteArquivo);
            formData.append(
                // tramiteArquivo.fluxoItemTipoAnexoId ? ('' + tramiteArquivo.fluxoItemTipoAnexoId) : '0',
                tramiteArquivoString,
                file,
                file.name);
        });

        // const blobOverrides = new Blob([JSON.stringify(tramiteArquivos)], {
        //     type: 'application/json',
        // });
        // formData.append('asd', blobOverrides);

        if (files.length > 0) {
            this._shellService.blockUI();
            return this._http.postDefault(`${this.apiEndpoint}/${uploadPath}`, this.options, formData)
                .pipe(
                    finalize(() => this._shellService.unblockUI()),
                    catchError((error) => {
                        return observableThrowError(error);
                    })
                );
        }
    }
}
