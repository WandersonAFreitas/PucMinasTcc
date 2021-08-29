import { Injectable } from '@angular/core';
import { AppAlertService } from '@fuse/services/app-alert/app-alert.service';
import { AppConfirmService } from '@fuse/services/app-confirm/app-confirm.service';
import { AppLoaderService } from '@fuse/services/app-loader/app-loader.service';

@Injectable()
export class ShellService {

  constructor(
    private appAlertService: AppAlertService,
    private appConfirmService: AppConfirmService,
    private appLoaderService: AppLoaderService,
  ) { }


  alert() {
    return this.appAlertService;
  }

  confirm() {
    return this.appConfirmService;
  }

  loader() {
    return this.appLoaderService;
  }

  blockUI() {
    setTimeout(() => {
      this.loader().open();
    }, 0);
  }

  unblockUI() {
    setTimeout(() => {
      this.loader().close();
    }, 10);
  }

  container() {
  }

  content() {
  }

}
