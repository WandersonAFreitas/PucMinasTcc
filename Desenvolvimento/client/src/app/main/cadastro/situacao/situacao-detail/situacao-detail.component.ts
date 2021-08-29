import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { SituacaoService } from '../situacao.service';
import { TipoSituacaoEnum } from '@fuse/types/models/enums/tipo-situacao-enum';
import { Situacao } from '@fuse/types/models/situacao';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  templateUrl: './situacao-detail.component.html',
  styleUrls: ['./situacao-detail.component.css']
})
export class SituacaoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  public situacaoTodas = TipoSituacaoEnum.Todas;
  public situacaoProximaSituacao = TipoSituacaoEnum.ProximaSituacao;
  public situacaoSituacaoAtual = TipoSituacaoEnum.SituacaoAtual;
  public situacaoSituacaoFinal = TipoSituacaoEnum.SituacaoFinal;

  private entity: Situacao;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: SituacaoService,
    private _shellService: ShellService
  ) {
    this.initialize();
    this.createFormValidators();
  }

  ngOnInit() {
    this.init();
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
          console.log(this.entity);
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
      nome: new FormControl(this.entity.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]),
      padrao: [this.entity.padrao],
      tipoSituacao: [this.entity.tipoSituacao]
    });
  }

  public save(formModel: Situacao, isValid: boolean): void {
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
          () => {
            this.goBack();
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Situacao): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new Situacao(null, null, null, 3);
  }
}
