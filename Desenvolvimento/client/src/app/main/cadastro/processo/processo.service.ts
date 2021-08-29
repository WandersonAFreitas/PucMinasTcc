import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { Processo } from '@fuse/types/models/processo';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { User } from '@fuse/types/models/user';
import { Tramite } from '@fuse/types/models/tramite';


@Injectable()
export class ProcessoService extends RestfulService<Processo> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'processo';
  }

  public setResponsavel(processoId: number, responsavelId: number): Observable<User> {
    return this._http.post(`/${this.apiUrl}/${processoId}/responsavel/${responsavelId}`, true);
  }

  public getQuantidadesDeProcessoPorResposanvel(responsavelId: number): Observable<any> {
    return this._http.get(`/${this.apiUrl}/quantidades/responsavel/${responsavelId}`, true);
  }


  public getTramiteValidoDoProcesso(id: number): Observable<Tramite> {
    return this._http.get(`/${this.apiUrl}/${id}/tramite/valido`, true);
  }

  public getReceber(id: number, idResponsavel: number): Observable<Processo> {
    return this._http.get(`/${this.apiUrl}/${id}/receber/resposavel/${idResponsavel}`, true);
  }
}
