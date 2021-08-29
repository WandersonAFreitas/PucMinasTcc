import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { Endereco } from '@fuse/types/models/endereco';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable()
export class EnderecoService extends RestfulService<Endereco> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'endereco';
  }
}
