import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { TipoMonitoramento } from '@fuse/types/models/tipo-monitoramento';

@Injectable()
export class TipoMonitoramentoService extends RestfulService<TipoMonitoramento> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'tipomonitoramento';
  }
}
