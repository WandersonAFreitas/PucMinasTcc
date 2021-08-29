import { Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpBaseService } from './http-base.service';
import { BaseModel } from '@fuse/types/base-model';
import { GridSettings } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';

export interface RestfulModel<T extends BaseModel> {
    model?: T;
    numberOfPage: number;
    registriesPerPage: number;
}

@Injectable()
export class RestfulService<T extends BaseModel> {

    protected apiUrl: string;

    constructor(public http: HttpBaseService, @Inject('API_URL_OVERRIDE') private apiUrlOverride: string = null) {
        if (this.apiUrlOverride) {
            this.apiUrl = this.apiUrlOverride;
        }
    }

    public overrideApiUrl(apiUrlOverride) {
        this.apiUrl = apiUrlOverride;
    }

    public getByFilter(gridSettings: GridSettings, blockUI = true): Observable<Paginacao<T>> {
        return this.http.post(`/${this.apiUrl}/find`, blockUI, gridSettings);
    }

    public get(id: number, blockUI = true): Observable<T> {
        return this.http.get(`/${this.apiUrl}/${id}`, blockUI);
    }

    public getPaginator(retfulModel: RestfulModel<T>, blockUI = true): Observable<Paginacao<T>> {
        return this.http.get(`/${this.apiUrl}?` +
            `${this.getNumberPage(retfulModel.numberOfPage)}&` +
            `${this.getNumberRegistries(retfulModel.registriesPerPage)}`, blockUI);
    }

    public getByModel(retfulModel: RestfulModel<T>, blockUI = true): Observable<Paginacao<T>> {
        return this.http.post(`/${this.apiUrl}/find?` +
            `${this.getNumberPage(retfulModel.numberOfPage)}&` +
            `${this.getNumberRegistries(retfulModel.registriesPerPage)}`, blockUI, retfulModel.model);
    }

    public getAll(blockUI = true): Observable<T[]> {
        return this.http.get(`/${this.apiUrl}`, blockUI);
    }

    public getUrl(apiUrl: string): Observable<T[]> {
        return this.http.get(`/${apiUrl}`, true);
    }

    public save(model: T, blockUI = true): Observable<T> {
        return this.http.post(`/${this.apiUrl}`, blockUI, model);
    }

    public update(model: T, blockUI = true): Observable<T> {
        return this.http.put(`/${this.apiUrl}`, blockUI, model);
    }

    public remove(id: number, blockUI = true): Observable<T> {
        return this.http.delete(`/${this.apiUrl}/${id}`, blockUI);
    }

    public setApiURL(apiUrl: string) {
        this.apiUrl = apiUrl;
    }

    private getNumberPage(numberOfPage: number): string {
        return `page=${numberOfPage ? numberOfPage : 0}`;
    }

    private getNumberRegistries(numberOfRegistries: number): string {
        return `size=${numberOfRegistries ? numberOfRegistries : 10}`;
    }
}
