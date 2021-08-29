import { Injectable } from "@angular/core";
import { RestfulService } from "@fuse/core/restful.service";
import { Acao } from "@fuse/types/models/acao";
import { HttpBaseService } from "@fuse/core/http-base.service";

@Injectable()
export class AcaoService extends RestfulService<Acao> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'acao';
  }
}
