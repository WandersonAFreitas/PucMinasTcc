import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Consultoria } from '@fuse/types/models/consultoria';

@Injectable()
export class ConsultoriaService extends RestfulService<Consultoria> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'consultoria';
  }
}
