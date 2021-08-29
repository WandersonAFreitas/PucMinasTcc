import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { MonitoramentoBarragem } from '@fuse/types/models/monitoramento-barragem';

@Injectable()
export class MonitoramentoBarragemService extends RestfulService<MonitoramentoBarragem> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'monitoramentobarragem';
  }
}
