import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Municipio } from '@fuse/types/models/municipio';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class MunicipioService extends RestfulService<Municipio> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'municipio';
  }
}
