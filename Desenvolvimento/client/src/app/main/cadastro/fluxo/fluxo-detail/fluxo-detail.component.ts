import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin, Observable } from 'rxjs';
import { finalize, map, startWith } from 'rxjs/operators';
import { FluxoService } from '../fluxo.service';
import { Fluxo } from '@fuse/types/models/fluxo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { AssuntoService } from '@fuse/core/assunto.service';

@Component({
  templateUrl: './fluxo-detail.component.html',
  styleUrls: ['./fluxo-detail.component.css']
})
export class FluxoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;
  public tabItem = false;

  private entity: Fluxo;
  public gridSettingsModel: GridSettings;

  constructor(
    private cdRef: ChangeDetectorRef,
    private _router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: FluxoService,
    private _shellService: ShellService,
    public _assuntoService: AssuntoService
  ) {
    this.initialize();
    this.createFormValidators();

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  ngOnInit() {
    this.init();
    this.cdRef.detectChanges();
  }

  private init(): void {
    this.route.params.subscribe(params => {
      this._shellService.blockUI();
      if (params.id) {
        this.id = params.id;
        forkJoin(
          this._service.get(this.id, false),
        ).pipe(
          finalize(() => this._shellService.unblockUI())
        ).subscribe(([entity]) => {
          this.entity = entity;
          this.createFormValidators();
        })
      } else {
        this._shellService.unblockUI();
        this.createFormValidators();
      }
    });
  }

  private createFormValidators(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      ativo: [this.entity.ativo],
      descricao: [this.entity.descricao],
      observacao: [this.entity.observacao],
      tramitarEm: [this.entity.tramitarEm]
    });
  }

  public save(formModel: Fluxo, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._service.update(this.entity).subscribe(
          () => {
            this.goBack();
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.entity).subscribe(
          (flxuo) => {
            this._router.navigate(['/cadastro/fluxo/edit', flxuo.id]);
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Fluxo): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this._router.navigate(['/cadastro/fluxo']);
  }

  public copiarFluxo(): void {
    this._shellService.confirm().confirm({ message: 'Deseja copiar o fluxo?', title: 'Confirmar' })
      .subscribe(res => {
        if (res) {
          this._service.copiaFluxo(this.entity.id).subscribe(
            (flxuo) => {
              this.goBack();
              this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
            });
        }
      });
  }

  private initialize(): void {
    this.entity = new Fluxo(null);
  }

  reciverTabItem(data) {
    this.tabItem = data;
  }
}
