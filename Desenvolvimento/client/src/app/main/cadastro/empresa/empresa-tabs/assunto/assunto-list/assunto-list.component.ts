import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { SetorService } from '../../../setor.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Assunto } from '@fuse/types/models/assunto';
import { Empresa } from '@fuse/types/models/empresa';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { AssuntoService } from '@fuse/core/assunto.service';
import { EmpresaTabAssuntoDetailComponent } from '@fuse/components/empresa/assunto/detail/assunto-detail.component';

@Component({
  selector: 'empresa-tab-assuntos',
  templateUrl: './assunto-list.component.html',
  styleUrls: ['./assunto-list.component.scss']
})
export class EmpresaAssuntoListComponent extends BaseListComponent<Assunto> implements OnInit {

  @Input() public empresa: Empresa;

  public statusFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  // @ViewChild('SetorInput') SetorInput: ElementRef;
  // public setores: Setor[];
  // private SetorObservable: Observable<Paginacao<Setor>>;
  // private subjectSetor: Subject<string> = new Subject<string>();

  public empresaId: number;
  public displayedColumns: string[] = ['Actions', 'Nome', 'Ativo'];

  public entityFilter: Assunto;

  public gridSettingsModelToFlterAutocomplete: GridSettings;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    public _setorService: SetorService,
    private _shellService: ShellService,
    private _assuntoService: AssuntoService
  ) {
    super(_router, _assuntoService, _shellService);
    this.empresaId = this.route.params['value'].id || 0;
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
    // this.initSetorSubject();
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
    this.initGridSettingsModel();
  }

  private initGridSettingsModel() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModelToFlterAutocomplete = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  private initialFilter(): Filter {
    const rules = [
      new Rule('EmpresaId', this.op.eq, '' + this.empresaId),
    ];
    // const filter = new Filter(this.group.and, rules);
    // const initialFilter = new Filter(this.group.and, new Array<Rule>(), [filter]);

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public delete(entity: Assunto): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Assunto: ${entity.nome}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Assunto removido com sucesso!'], timeout: 3000 });
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
        });
      }
    });
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear(this.initialFilter());
  }

  private initFilter(): Assunto {
    const model: Assunto = new Assunto(null);
    return model;
  }

  public add(): void {
    const empresaId = this.empresaId;
    const dialogRef = this._dialog.open(EmpresaTabAssuntoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        empresaId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(assunto: Assunto): void {
    const empresaId = this.empresaId;
    const dialogRef = this._dialog.open(EmpresaTabAssuntoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        assunto,
        empresaId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  private afterClosedRefreshGrids(dialogRef: any) {
    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Ok') {
        this.search();
      }
    });
  }



  // public searchSetores(email: string): void {
  //   this.subjectSetor.next(email);
  // }

  // private initSetorSubject(): void {
  //   this.SetorObservable = this.subjectSetor
  //     .pipe(
  //       debounceTime(500),
  //       distinctUntilChanged(),
  //       switchMap((termo: string) => {
  //         const rule = new Rule('Nome', 'cn', termo);
  //         const filter = new Filter(0, [rule]);
  //         const gridSettingsModel: GridSettings = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  //         return this._setorService.getByFilter(gridSettingsModel, false);
  //       })
  //     );

  //   this.SetorObservable.subscribe(paginacao => {
  //     this.setores = paginacao.content;
  //   });
  // }

  // public optionSelectedSetor(event: MatAutocompleteSelectedEvent) {
  //   const viewValue = event.option.viewValue;
  //   this.SetorInput.nativeElement['value'] = viewValue;
  // }
}
