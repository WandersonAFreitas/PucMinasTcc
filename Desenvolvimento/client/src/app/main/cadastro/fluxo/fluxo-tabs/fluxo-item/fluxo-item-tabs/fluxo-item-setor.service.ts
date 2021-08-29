import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { FluxoItemSetor } from '@fuse/types/models/fluxo-item-setor';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class FluxoItemSetorService extends RestfulService<FluxoItemSetor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxoitem/setor';
  }

  public getItemSetoresPorFluxo(id: number): Observable<FluxoItemSetor[]> {
    return this._http.get(`/${this.apiUrl}/item-setores/fluxo/${id}`, true);
  }
}
