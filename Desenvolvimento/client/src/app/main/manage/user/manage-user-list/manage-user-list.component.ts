import {  Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { ManageService } from '../../manage.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { ManageUser } from '@fuse/types/models/manage-user';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { Rule, Filter, GridSettings } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-manage',
  templateUrl: './manage-user-list.component.html',
  styleUrls: ['./manage-user-list.component.scss']
})
export class ManageUserListComponent extends BaseListComponent<ManageUser> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'UserName', 'Email', 'Roles'];

  public entityFilter: ManageUser;
  public entities: ManageUser[] = [];
  public userObservable: Observable<Paginacao<ManageUser>>;
  private subjectPesquisaUserEmail: Subject<string> = new Subject<string>();

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _manageService: ManageService
  ) {
    super(_router, _manageService, _shellService, 'manage/user');
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Name', 'asc');
    this.initUserSubject();
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/manage/user/new']);
  }

  public edit(entity: ManageUser): void {
    this.baseEdit(['/manage/user/edit', entity.id]);
  }

  public delete(entity: ManageUser): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o User: ${entity.userName}?`,
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

  private initFilter(): ManageUser {
    const model: ManageUser = new ManageUser(null);
    return model;
  }

  public searchUserEmails(email: string): void {
    this.subjectPesquisaUserEmail.next(email);
  }

  private initUserSubject(): void {
    this.userObservable = this.subjectPesquisaUserEmail
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
        switchMap((termo: string) => {
          const rule = new Rule('Email', 'cn', termo);
          const filter = new Filter(0, [rule]);
          const gridSettingsModel: GridSettings = new GridSettings(true, 1, 10, 'Email', 'asc', filter);
          return this.service.getByFilter(gridSettingsModel);
        })
      );

    this.userObservable.subscribe((paginacao: Paginacao<ManageUser>) => {
      this.entities = paginacao.content;
    });
  }
}
