import { Injectable } from '@angular/core';
import { RestfulService } from './restful.service';
import { AssuntoArquivo } from '@fuse/types/models/assunto-arquivo';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class AssuntoArquivoService extends RestfulService<AssuntoArquivo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'assuntoArquivo';
  }
}
