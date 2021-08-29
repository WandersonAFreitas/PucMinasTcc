import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { TipoInsumo } from '@fuse/types/models/tipo-insumo';

@Injectable()
export class TipoInsumoService extends RestfulService<TipoInsumo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'tipoinsumo';
  }
}
