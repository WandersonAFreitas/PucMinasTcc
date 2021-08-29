import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { FuseConfigService } from '@fuse/services/config.service';

@Injectable()
export class ResolverService implements Resolve<any> {

  constructor(private _fuseConfigService: FuseConfigService) { }

  resolve() {    
    this._fuseConfigService.config = {
        layout: {
            navbar: {
                hidden: true
            },
            toolbar: {
                hidden: true
            },
            footer: {
                hidden: true
            },
            sidepanel: {
                hidden: true
            }
        }
    };

    return 'some string';
  }

}