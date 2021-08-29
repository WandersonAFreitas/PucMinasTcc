import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LogradouroService } from '../logradouro.service';
import { Logradouro } from '@fuse/types/models/logradouro';
import { ShellService } from '@fuse/core/shell.service';
import { Pais } from '@fuse/types/models/pais';
import { Estado } from '@fuse/types/models/estado';
import { Municipio } from '@fuse/types/models/municipio';

@Component({
  templateUrl: './logradouro-detail.component.html',
  styleUrls: ['./logradouro-detail.component.css']
})
export class LogradouroDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  private entity: Logradouro;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: LogradouroService,
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
      cep: new FormControl(this.entity.cep, [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
      endereco: new FormControl(this.entity.endereco, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      bairro: new FormControl(this.entity.bairro, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      pais: new FormControl(this.entity.pais.nome),
      estado: new FormControl(this.entity.estado.nome),
      municipio: new FormControl(this.entity.municipio.nome),
    });
  }

  public save(formModel: Logradouro, isValid: boolean): void {
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

  private prepareToSave(formModel: Logradouro): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new Logradouro(null);
    this.entity.pais = new Pais(null);
    this.entity.estado = new Estado(null);
    this.entity.municipio = new Municipio(null);
  }
}
