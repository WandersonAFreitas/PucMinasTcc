import { Injectable } from '@angular/core';
import { RestfulService } from './restful.service';
import { TramiteArquivo } from '@fuse/types/models/tramite-arquivo';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class TramiteArquivoService extends RestfulService<TramiteArquivo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'tramiteArquivo';
  }
}
