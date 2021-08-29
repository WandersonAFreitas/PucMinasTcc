import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Empresa } from '@fuse/types/models/empresa';


@Injectable()
export class EmpresaService extends RestfulService<Empresa> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'empresa';
  }
}
