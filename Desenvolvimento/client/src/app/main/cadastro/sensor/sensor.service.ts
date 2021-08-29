import { Injectable } from '@angular/core';
import { RestfulService } from '@fuse/core/restful.service';
import { HttpBaseService } from '@fuse/core/http-base.service';
import { Sensor } from '@fuse/types/models/sensor';

@Injectable()
export class SensorService extends RestfulService<Sensor> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'sensor';
  }
}
