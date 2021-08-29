import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Marca } from '@fuse/types/models/marca';

@Injectable()
export class MarcaService extends RestfulService<Marca> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'marca';
  }
}
