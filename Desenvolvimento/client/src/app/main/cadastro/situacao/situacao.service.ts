import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Situacao } from '@fuse/types/models/situacao';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class SituacaoService extends RestfulService<Situacao> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'situacao';
  }
}
