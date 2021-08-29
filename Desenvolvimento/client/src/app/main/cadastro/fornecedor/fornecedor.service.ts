import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Fornecedor } from '@fuse/types/models/fornecedor';

@Injectable()
export class FornecedorService extends RestfulService<Fornecedor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fornecedor';
  }
}
