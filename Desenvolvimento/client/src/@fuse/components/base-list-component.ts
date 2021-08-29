import { OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatPaginator, MatSort, Sort, MatSortable } from '@angular/material';
import { BaseModel } from '@fuse/types/base-model';
import { WHERE_OPERATION_FILTER } from '@fuse/types/models/enums/where-operation-enum';
import { GROUP_OPERATION_FILTER } from '@fuse/types/models/enums/group-operation-enum';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';
import { GridSettings, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { RestfulService } from '@fuse/core/restful.service';
import { ShellService } from '@fuse/core/shell.service';
import { rowsNumberPerPage } from 'assets/strings';

export class BaseListComponent<T extends BaseModel> {

  public op = WHERE_OPERATION_FILTER;
  public group = GROUP_OPERATION_FILTER;
  public dataSource: MatTableDataSource<T>;
  public pagination: Paginacao<T>;
  public gridSettingsModel: GridSettings;
  public isLoadingResults = false;

  private _sidx = 'Id';
  private _sord = 'asc';

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    protected router: Router,
    protected service: RestfulService<T>,
    protected shellService: ShellService,
    protected apiUrlOverride?: string,
  ) {
    this.pagination = new Paginacao<T>();
    if (apiUrlOverride) {
      service.overrideApiUrl(apiUrlOverride);
    }
  }

  private baseInitPaginator(): void {
    if (this.paginator) {
      this.paginator.page.subscribe(() => {
        this.gridSettingsModel.page = +this.paginator.pageIndex + 1;
        this.gridSettingsModel.rows = this.paginator.pageSize || rowsNumberPerPage;
        this.baseSearch(this.gridSettingsModel);
      });
    }
  }

  protected baseLoad(model: GridSettings, startLoad = true): void {
    this.baseInitPaginator();
    this.isLoadingResults = true;
    if (startLoad) {
      this.service.getByFilter(model).subscribe(objectPaginator => {
        this.pagination = objectPaginator;
        this.dataSource = new MatTableDataSource<T>(this.pagination.content);
        this.isLoadingResults = false;
      });
    }
  }

  protected baseEdit(commands: any[]): void {
    this.router.navigate(commands);
  }

  protected baseDelete(model: any): void {
    this.shellService.confirm().confirm({ message: model.mensagem, title: model.titulo })
      .subscribe(res => {
        if (res) {
          model.callback();
        }
      })
  }

  protected baseSearch(model: GridSettings, callback: Function = null): void {
    this.isLoadingResults = true;
    this.service.getByFilter(model).subscribe(objectPaginator => {
      this.pagination = objectPaginator;
      this.paginator.pageIndex = this.pagination.number - 1;
      this.dataSource = new MatTableDataSource<T>(this.pagination.content);
      this.isLoadingResults = false;
      callback && callback(this.dataSource);
    });
  }

  protected baseClear(startFilter: Filter = null, pageSize: number = null): void {
    this.gridSettingsModel = this.baseInitGridSettings(startFilter, this._sidx, this._sord, pageSize);
    // TODO: checar dubpla chamada de baseSearch ( baseResetSort -> baseSortData -> baseSearch )
    this.baseSearch(this.gridSettingsModel);
    this.baseResetSort();
  }

  protected baseResetSort(): void {
    if (this.sort.active !== this._sidx || this.sort.direction !== this._sord) {
      this.sort.sort(<MatSortable>{
        id: this._sidx,
        start: this._sord
      }
      );
    }
  }

  public baseSortData(sort: Sort) {
    if (!sort.active || sort.direction === '') {
      this.gridSettingsModel.sidx = this._sidx;
      this.gridSettingsModel.sord = this._sord;
      this.baseSearch(this.gridSettingsModel);
      return;
    }
    this.gridSettingsModel.sidx = sort.active;
    this.gridSettingsModel.sord = sort.direction;
    this.baseSearch(this.gridSettingsModel);
  }

  protected baseInitGridSettings(startFilter: Filter = null, sidx: string = this._sidx, sord: string = this._sord, pageSize: number = null): GridSettings {
    this._sidx = sidx;
    this._sord = sord;
    const model: GridSettings = new GridSettings(true, 1, pageSize || rowsNumberPerPage, this._sidx, this._sord);
    if (startFilter) {
      model.filters = startFilter;
    }
    return model;
  }

  protected baseSetToFistPageGridSettings(): GridSettings {
    return (this.gridSettingsModel.page = 1) && this.gridSettingsModel;
  }
}
