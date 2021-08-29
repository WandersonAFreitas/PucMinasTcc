import {  Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ManageService } from '../../manage.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { ManagerRole } from '@fuse/types/models/manage-user';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-manage-role-list',
  templateUrl: './manage-role-list.component.html',
  styleUrls: ['./manage-role-list.component.scss']
})
export class ManageRoleListComponent extends BaseListComponent<ManagerRole> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Name'];

  public entityFilter: ManagerRole;
  public entities: ManagerRole[] = [];
  public userObservable: Observable<Paginacao<ManagerRole>>;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _manageService: ManageService
  ) {
    super(_router, _manageService, _shellService, 'manage/role');
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Name', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/manage/role/new']);
  }

  public edit(entity: ManagerRole): void {
    this.baseEdit(['/manage/role/edit', entity.id]);
  }

  public delete(entity: ManagerRole): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir a role: ${entity.name}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
          this.shellService.alert().success({ messages: ['Deletado com sucesso!'], timeout: 3000 });
        });
      }
    });
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): ManagerRole {
    const model: ManagerRole = new ManagerRole(null);
    return model;
  }
}
