import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { TipoAnexo } from '@fuse/types/models/tipo-anexo';

@Injectable()
export class TipoAnexoService extends RestfulService<TipoAnexo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'tipoanexo';
  }
}
