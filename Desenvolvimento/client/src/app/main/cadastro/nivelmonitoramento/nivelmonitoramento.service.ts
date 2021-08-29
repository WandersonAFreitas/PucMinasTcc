import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { NivelMonitoramento } from '@fuse/types/models/nivel-monitoramento';

@Injectable()
export class NivelMonitoramentoService extends RestfulService<NivelMonitoramento> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'nivelmonitoramento';
  }
}
