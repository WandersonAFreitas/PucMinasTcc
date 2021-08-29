import { AppAlertComponent, FlashMessageObject } from './app-alert.component';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class AppAlertService {

  public alertChange: Subject<FlashMessageObject> = new Subject<FlashMessageObject>();

  constructor() {}

  public success(flashMessageObject: FlashMessageObject) {
    flashMessageObject.messageType = AppAlertComponent.MESSAGES_TYPES.SUCCESS;
    this.alertChange.next(flashMessageObject);
  }

  public info(flashMessageObject: FlashMessageObject) {
    flashMessageObject.messageType = AppAlertComponent.MESSAGES_TYPES.INFO;
    this.alertChange.next(flashMessageObject);
  }

  public warning(flashMessageObject: FlashMessageObject) {
    flashMessageObject.messageType = AppAlertComponent.MESSAGES_TYPES.WARNING;
    this.alertChange.next(flashMessageObject);
  }

  public error(flashMessageObject: FlashMessageObject) {
    flashMessageObject.messageType = AppAlertComponent.MESSAGES_TYPES.ERROR;
    this.alertChange.next(flashMessageObject);
  }

}
