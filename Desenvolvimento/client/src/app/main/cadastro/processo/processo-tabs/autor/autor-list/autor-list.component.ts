import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material';
import { SetorService } from '../../../setor.service';
import { ProcessoTabAutorDetailComponent } from '../autor-detail/autor-detail.component';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { ProcessoAutor } from '@fuse/types/models/processo-autor';
import { Processo } from '@fuse/types/models/processo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { Autor } from '@fuse/types/models/autor';
import { AutorService } from '@fuse/core/autor.service';
import { ProcessoAutorService } from '@fuse/core/processo-autor.service';

@Component({
  selector: 'processo-tab-autores',
  templateUrl: './autor-list.component.html',
  styleUrls: ['./autor-list.component.css']
})
export class ProcessoAutorListComponent extends BaseListComponent<ProcessoAutor> implements OnInit {

  @Input() public processo: Processo;

  public formGroupAutor: FormGroup;
  public gridSettingsModelAutor: GridSettings;

  public processoId: number;
  public displayedColumns: string[] = ['Actions', 'Autor.Nome', 'Autor.Cpf'];

  public entityFilter: ProcessoAutor;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _fb: FormBuilder,
    public _setorService: SetorService,
    private _shellService: ShellService,
    public _autorService: AutorService,
    private _processoAutorService: ProcessoAutorService
  ) {
    super(_router, _processoAutorService, _shellService);
    this.processoId = this.route.params['value'].id || 0;
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Autor.Nome', 'asc');
  }

  private createFormValidatorsAutor(): void {
    this.formGroupAutor = this._fb.group({
      autorNome: [],
      autorId: [],
    });
  }

  private initGridSettingsModelAutor() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Cpf', 'eq', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModelAutor = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
    this.initGridSettingsModelAutor();
    this.createFormValidatorsAutor();
  }

  private initialFilter(): Filter {
    const rules = [
      new Rule('ProcessoId', this.op.eq, '' + this.processoId),
    ];

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public delete(entity: ProcessoAutor): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente remover o Autor "${entity.autor.nome}" do processo atual?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Autor removido com sucesso!'], timeout: 3000 });
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

  private initFilter(): ProcessoAutor {
    const model: ProcessoAutor = new ProcessoAutor(null);
    model.autor = new Autor(null);
    return model;
  }


  public addAutorSelecionado(): void {
    const autorId = this.formGroupAutor.controls.autorId.value;
    const processoId = this.processoId;
    const newProcessoAutor = new ProcessoAutor(null);
    newProcessoAutor.autorId = autorId;
    newProcessoAutor.processoId = processoId;
    this._processoAutorService.save(newProcessoAutor).subscribe(x => {
      this._shellService.alert().success({ messages: ['O Autor selecionado foi adicionado com sucesso!'], timeout: 3000 });
      this.search();
    });
  }

  public addNovoAutor(): void {
    const processoId = this.processoId;
    const dialogRef = this._dialog.open(ProcessoTabAutorDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        processoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(processoAutor: ProcessoAutor): void {
    const processoId = this.processoId;
    const dialogRef = this._dialog.open(ProcessoTabAutorDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        autor: processoAutor.autor,
        processoId
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
}
