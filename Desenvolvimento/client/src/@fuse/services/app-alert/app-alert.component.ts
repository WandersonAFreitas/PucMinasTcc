import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

export interface FlashMessageObject {
  messages: string[];
  timeout: number;
  messageType?: number;
}

@Component({
  selector: 'app-alert',
  template: `
    <div class="custom-alert">
      <div [id]="'alerta_' + i" #alertDiv *ngFor="let alert of alerts;let i = index;" class="alert-div animated slideInDown"
           [ngClass]="alert.type">
        <button class="alert-close" mat-icon-button (click)="closeMe(alert, alertDiv.id)">
          <mat-icon>close</mat-icon>
        </button>
        <h3>
          <mat-icon>{{ iconMapping[alert.type].icon }}</mat-icon>
          {{ iconMapping[alert.type].header.toUpperCase() }}
        </h3>
        <div class="alert-content" *ngFor="let m of alert.msgs" [innerHTML]="content(m)"></div>
      </div>
    </div>
  `,
  styleUrls: ['./app-alert.component.scss']
})
export class AppAlertComponent implements OnInit {

  public static MESSAGES_TYPES = {
    SUCCESS: 1,
    INFO: 2,
    WARNING: 3,
    ERROR: 4
  };

  alerts: any[] = [];

  private typeMapping = {
    1: ['success'],
    2: ['info'],
    3: ['warning'],
    4: ['error']
  };

  private iconMapping = {
    'success': { icon: 'check_circle', header: 'Sucesso!' },
    'info': { icon: 'info_outline', header: 'Importante:' },
    'warning': { icon: 'warning', header: 'Atenção!' },
    'error': { icon: 'error_outline', header: 'Erro:' },
  }

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() { }

  public open(flashMessageObject: FlashMessageObject) {
    this.alerts.push({
      type: this.typeMapping[flashMessageObject.messageType],
      msgs: flashMessageObject.messages,
      timeout: flashMessageObject.timeout
    });

    this.alerts.forEach(
      (a) => {
        setTimeout(
          () => {
            if (this.alerts.length > 0) {
              this.closeMe(a, `alerta_${this.alerts.indexOf(a)}`);
            }
          }, a.timeout);
      }
    );

  }

  private content(message: string) {
    return this.sanitizer.bypassSecurityTrustHtml(message);
  }

  private closeMe(alert: any, alertId: any) {
    const el: HTMLElement = document.getElementById(alertId);
    if (el) {
      el.classList.remove('slideInDown');
      el.classList.add('fadeOut');
    }

    setTimeout(
      () => {
        const index = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
      }, 1000
    );
  }
}
