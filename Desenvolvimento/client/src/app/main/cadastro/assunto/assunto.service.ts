import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Assunto } from '@fuse/types/models/assunto';
import { HttpBaseService } from '@fuse/core/http-base.service';


@Injectable()
export class AssuntoService extends RestfulService<Assunto> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'assunto';
  }
}
