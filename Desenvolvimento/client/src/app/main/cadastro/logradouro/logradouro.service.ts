import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Logradouro } from '@fuse/types/models/logradouro';

@Injectable()
export class LogradouroService extends RestfulService<Logradouro> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'logradouro';
  }

  public getLogradouro(cep: string): Observable<Logradouro> {
    return this.http.post(`/${this.apiUrl}/GetLogradouro/${cep}`, true);
  }
}
