import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { TipoAnexoService } from '../tipoanexo.service';
import { TipoAnexo } from '@fuse/types/models/tipo-anexo';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  templateUrl: './tipoanexo-detail.component.html',
  styleUrls: ['./tipoanexo-detail.component.css']
})
export class TipoAnexoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  private entity: TipoAnexo;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: TipoAnexoService,
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
      nome: new FormControl(this.entity.nome, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]),
    });
  }

  public save(formModel: TipoAnexo, isValid: boolean): void {
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

  private prepareToSave(formModel: TipoAnexo): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new TipoAnexo(null);
  }
}
