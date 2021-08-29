import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestfulService } from '@fuse/core/restful.service';
import { ManageUser } from '@fuse/types/models/manage-user';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { IChangePassword } from '@fuse/types/interfaces/i-change-password';


@Injectable()
export class ManageService extends RestfulService<ManageUser> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'manage';
  }

  public OverrideApiUrl(apiUrlOverride: string) {
    this.apiUrl = apiUrlOverride;
  }

  public changeCurrentPassword(model: IChangePassword, blockUI = true): Observable<any> {
    return this.http.post(`/manage/current/alterar/password`, blockUI, model);
  }
}
