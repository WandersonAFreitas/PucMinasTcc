import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from './restful.service';
import { Tramite } from '@fuse/types/models/tramite';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class TramiteService extends RestfulService<Tramite> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'tramite';
  }

  public getTramitar(id: number): Observable<Tramite> {
    return this._http.get(`/${this.apiUrl}/${id}/tramitar`, true);
  }


  public getHaTramiteNaoTramitadoNoProcesso(idProcesso: number): Observable<boolean> {
    return this._http.get(`/${this.apiUrl}/nao-tramitado/processo/${idProcesso}`, true);
  }
}
