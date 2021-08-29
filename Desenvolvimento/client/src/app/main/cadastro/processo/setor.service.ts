import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Setor } from '@fuse/types/models/setor';
import { HttpBaseService } from '@fuse/core/http-base.service';


@Injectable()
export class SetorService extends RestfulService<Setor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'setor';
  }
}
