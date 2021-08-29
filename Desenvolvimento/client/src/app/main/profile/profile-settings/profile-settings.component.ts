import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { ShellService } from '@fuse/core/shell.service';
import { ManageService } from 'app/main/manage/manage.service';
import { IChangePassword } from '@fuse/types/interfaces/i-change-password';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent implements OnInit {

  public formGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private _shellService: ShellService,
    private _manageService: ManageService
  ) {
    this.formGroup = this.formBuilder.group({
      oldPassword: [null, [Validators.required]],
      newPassword: [null, [Validators.required]],
      checkPassword: [null, [Validators.required]]
    });
  }

  ngOnInit() {
  }

  public save(formModel: IChangePassword, isValid: boolean): void {
    this._manageService.changeCurrentPassword(formModel).subscribe(
      () => {
        this._shellService.alert().success({ messages: ['Password atualizado com sucesso!'], timeout: 3000 });
        this.formGroup.reset();
      });
  }

}
