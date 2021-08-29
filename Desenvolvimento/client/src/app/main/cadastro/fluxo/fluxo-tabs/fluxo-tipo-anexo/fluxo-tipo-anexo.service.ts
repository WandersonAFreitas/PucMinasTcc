import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { FluxoTipoAnexo } from '@fuse/types/models/fluxo-tipo-anexo';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class FluxoTipoAnexoService extends RestfulService<FluxoTipoAnexo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxo/tipoanexo';
  }
}
