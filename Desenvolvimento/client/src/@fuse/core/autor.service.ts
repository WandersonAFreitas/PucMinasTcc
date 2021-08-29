import { Injectable } from '@angular/core';
import { RestfulService } from './restful.service';
import { Autor } from '@fuse/types/models/autor';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class AutorService extends RestfulService<Autor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'autor';
  }
}
