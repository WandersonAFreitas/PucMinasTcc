import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Pais } from '@fuse/types/models/pais';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class PaisService extends RestfulService<Pais> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'pais';
  }
}
