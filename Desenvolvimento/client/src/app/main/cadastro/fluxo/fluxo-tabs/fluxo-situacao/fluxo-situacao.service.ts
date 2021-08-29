import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Situacao } from '@fuse/types/models/situacao';

@Injectable({
  providedIn: 'root'
})
export class FluxoSituacaoService extends RestfulService<Situacao> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxo/situacao';
  }
}
