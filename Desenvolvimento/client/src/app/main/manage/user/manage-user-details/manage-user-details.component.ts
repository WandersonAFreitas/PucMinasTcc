import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatTableDataSource, MatButtonToggleChange } from '@angular/material';
import { CustomValidators } from 'ng2-validation';
import { ManageService } from '../../manage.service';
import { ManagerRole, ManageUser } from '@fuse/types/models/manage-user';
import { ManageUserSetor } from '@fuse/types/models/manage-user-setor';
import { ShellService } from '@fuse/core/shell.service';
import { HttpBaseService } from '@fuse/core/http-base.service';


@Component({
  selector: 'app-manage-user-details',
  templateUrl: './manage-user-details.component.html',
  styleUrls: ['./manage-user-details.component.scss']
})
export class ManageUserDetailsComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  public dataUserRole: ManagerRole[];
  public dataUserSetor: ManageUserSetor[];

  private entity: ManageUser;
  private shellService: ShellService;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: ManageService,
    private _http: HttpBaseService,
    shellService: ShellService
  ) {
    this._service.OverrideApiUrl('manage/user');
    this.initialize();
    this.createFormValidators();
    this.shellService = shellService;
  }

  ngOnInit() {
    this.initEntity();
  }

  private initEntity(): void {
    this.route.params.subscribe(params => {
      if (params.id) {
        this.id = params.id;
        this._service.get(this.id).subscribe((entity: ManageUser) => {
          this.entity = entity;
          this.createFormValidators();
          // this.getAllRoles();
        });
      // } else {
      //   this.getAllRoles();
      }
    });
  }

  private createFormValidators(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      userName: new FormControl(this.entity.userName, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]),
      email: new FormControl(this.entity.email, CustomValidators.email),
    });
  }

  public save(formModel: ManageUser, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._service.update(this.entity).subscribe(
          () => {
            this.goBack();
            this.shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.entity).subscribe(
          () => {
            this.goBack();
            this.shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: ManageUser): void {
    this.entity = { ...this.entity, ...formModel };

    this.entity.roles = this.dataUserRole.filter(r => JSON.parse(r.enabled.toString())).map(r => new ManagerRole(r.id, r.name, true));
    this.entity.userSetores = this.dataUserSetor.filter(r => JSON.parse(r.enabled.toString())).map(r => new ManageUserSetor(r.id, r.userId, r.setorId));
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new ManageUser(null);
  }

  reciverDataUserSetor(data) {
    this.dataUserSetor = data;
  }

  reciverDataUserRole(data) {
    this.dataUserRole = data;
  }

}
