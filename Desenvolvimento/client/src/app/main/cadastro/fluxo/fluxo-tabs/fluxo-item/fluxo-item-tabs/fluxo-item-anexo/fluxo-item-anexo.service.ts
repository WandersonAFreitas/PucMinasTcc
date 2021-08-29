import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { FluxoItemAnexo } from '@fuse/types/models/fluxo-item-anexo';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class FluxoItemAnexoService extends RestfulService<FluxoItemAnexo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxoitem/anexo';
  }
}
