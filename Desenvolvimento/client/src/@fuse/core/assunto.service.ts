import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from './restful.service';
import { HttpBaseService } from './http-base.service';
import { Assunto } from '@fuse/types/models/assunto';

@Injectable()
export class AssuntoService extends RestfulService<Assunto> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'assunto';
  }

  public putDesvincularFluxo(id: number): Observable<Assunto> {
    return this._http.get(`/${this.apiUrl}/desvincularfluxo/${id}`, true);
  }
}
