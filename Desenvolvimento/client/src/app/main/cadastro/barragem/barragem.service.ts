import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Barragem } from '@fuse/types/models/barragem';

@Injectable()
export class BarragemService extends RestfulService<Barragem> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'barragem';
  }
}
