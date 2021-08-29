import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material';
import { ManageService } from '../../manage.service';
import { ManagerRole } from '@fuse/types/models/manage-user';
import { ShellService } from '@fuse/core/shell.service';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Component({
  selector: 'app-manage-role-details',
  templateUrl: './manage-role-details.component.html',
  styleUrls: ['./manage-role-details.component.scss']
})
export class ManageRoleDetailsComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;
  public dataSourceRoles: MatTableDataSource<ManagerRole>;
  public displayedColumnsRoles: string[] = ['Id', 'Name', 'Check'];

  private entity: ManagerRole;
  private shellService: ShellService;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: ManageService,
    private _http: HttpBaseService,
    shellService: ShellService
  ) {
    this._service.OverrideApiUrl('manage/role');
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
        this._service.get(this.id).subscribe((entity: ManagerRole) => {
          this.entity = entity;
          this.createFormValidators();
        });
      }
    });
  }

  private createFormValidators(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      name: new FormControl(this.entity.name, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]),
    });
  }

  public save(formModel: ManagerRole, isValid: boolean): void {
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

  private prepareToSave(formModel: ManagerRole): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new ManagerRole(null);
  }
}
