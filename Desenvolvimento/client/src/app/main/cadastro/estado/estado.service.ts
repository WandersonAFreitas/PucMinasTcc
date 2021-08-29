import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Estado } from '@fuse/types/models/estado';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class EstadoService extends RestfulService<Estado> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'estado';
  }
}
