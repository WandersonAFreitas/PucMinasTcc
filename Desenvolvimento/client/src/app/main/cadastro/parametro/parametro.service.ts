import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Parametro } from '@fuse/types/models/parametro';

@Injectable()
export class ParametroService extends RestfulService<Parametro> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'parametro';
  }
}
