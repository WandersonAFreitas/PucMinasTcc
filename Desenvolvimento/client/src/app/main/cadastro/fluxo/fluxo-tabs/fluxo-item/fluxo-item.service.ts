import { Injectable, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { FluxoItem } from '@fuse/types/models/fluxo-item';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class FluxoItemService extends RestfulService<FluxoItem> {

  static fluxoItemId: number;

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxoitem';
  }

  public getFluxoItensPorFluxo(fluxoId: number, acaoId: number): Observable<FluxoItem[]> {
    return this._http.get(`/${this.apiUrl}/todos/fluxo/${fluxoId}/acao/${acaoId}`, true);
  }

  public getFluxoItensPorFluxoEsituacao(fluxoId: number, situacaoId: number): Observable<FluxoItem[]> {
    return this._http.get(`/${this.apiUrl}/todos/fluxo/${fluxoId}/situacao/${situacaoId}`, true);
  }
}
