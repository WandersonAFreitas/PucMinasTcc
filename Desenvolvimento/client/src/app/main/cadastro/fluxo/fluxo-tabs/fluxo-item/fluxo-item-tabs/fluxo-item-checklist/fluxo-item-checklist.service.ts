import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { FluxoItemCheckList } from '@fuse/types/models/fluxo-item-checklist';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class FluxoItemChecklistService extends RestfulService<FluxoItemCheckList> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'fluxoitem/checklist';
  }
}
