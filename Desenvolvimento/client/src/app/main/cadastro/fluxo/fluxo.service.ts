import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { Fluxo } from '@fuse/types/models/fluxo';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class FluxoService extends RestfulService<Fluxo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxo';
  }

  public copiaFluxo(id: number): Observable<Fluxo> {
    return this.http.post(`/${this.apiUrl}/copiafluxo/${id}`, true);
  }
}
