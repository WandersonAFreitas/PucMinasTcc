import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { UnidadeMedida } from '@fuse/types/models/unidade-medida';

@Injectable()
export class UnidadeMedidaService extends RestfulService<UnidadeMedida> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'unidademedida';
  }
}
