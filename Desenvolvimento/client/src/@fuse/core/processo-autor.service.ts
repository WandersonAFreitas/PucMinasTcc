import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from './restful.service';
import { ProcessoAutor } from '@fuse/types/models/processo-autor';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class ProcessoAutorService extends RestfulService<ProcessoAutor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'processoAutor';
  }

  public putDesvincularFluxo(id: number): Observable<ProcessoAutor> {
    return this._http.get(`/${this.apiUrl}/desvincularfluxo/${id}`, true);
  }
}
