import { Injectable } from '@angular/core';
import { RestfulService } from './restful.service';
import { User } from '@fuse/types/models/user';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class UserService extends RestfulService<User> {

  constructor(private _http: HttpBaseService) {
    super(_http);
    this.apiUrl = 'user';
  }
}
