import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Insumo } from '@fuse/types/models/insumo';

@Injectable()
export class InsumoService extends RestfulService<Insumo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'insumo';
  }
}
