import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Setor } from '@fuse/types/models/setor';


@Injectable()
export class SetorService extends RestfulService<Setor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'setor';
  }
}
