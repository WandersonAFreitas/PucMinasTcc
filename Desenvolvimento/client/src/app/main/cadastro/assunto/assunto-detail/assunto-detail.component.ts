import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList, MatTableDataSource } from '@angular/material';
import { AssuntoService } from '../assunto.service';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { IArquivo } from '@fuse/types/interfaces/i-arquivo';
import { Assunto } from '@fuse/types/models/assunto';
import { ShellService } from '@fuse/core/shell.service';
import { ArquivoService } from '@fuse/core/arquivo.service';


@Component({
  templateUrl: './assunto-detail.component.html',
  styleUrls: ['./assunto-detail.component.css']
})
export class AssuntoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  public displayedColumnsArquivos: string[] = ['ArquivoNome', 'Actions'];
  public dataSourceArquivos: MatTableDataSource<IArquivo>;
  public arquivos = new Array<IArquivo>();

  private entity: Assunto;

  constructor(
    private location: Location,
    private _arquivoService: ArquivoService,
    private route: ActivatedRoute,
    private _fb: FormBuilder,
    private _service: AssuntoService,
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

          this.arquivos = this.entity.assuntoArquivos && this.entity.assuntoArquivos.map(x => <IArquivo>{
            id: x.arquivo.id,
            nome: x.arquivo.nome,
            hash: x.arquivo.hash
          });
          this.dataSourceArquivos = new MatTableDataSource<IArquivo>(this.arquivos);

          this.createFormValidators();
        })
      } else {
        this._shellService.unblockUI();
        this.createFormValidators();
      }
    });
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.entity.id, disabled: true }],
      empresaId: [this.entity.empresaId],
      empresaNome: [this.entity.empresa && this.entity.empresa.nome],
      // TODO: REMOVER
      // setorProcessoFisicoId: [this.entity.setorProcessoFisicoId],
      // setorProcessoFisicoNome: [this.entity.setorProcessoFisico && (this.entity.setorProcessoFisico.sigla + ' - ' + this.entity.setorProcessoFisico.nome)],
      // setorProcessoVirtualId: [this.entity.setorProcessoVirtualId],
      // setorProcessoVirtualNome: [this.entity.setorProcessoVirtual && (this.entity.setorProcessoVirtual.sigla + ' - ' + this.entity.setorProcessoVirtual.nome)],
      ativo: [this.entity.ativo],
      orientacoes: [this.entity.orientacoes],
      nome: [this.entity.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
    });
  }

  // public save(formModel: Assunto, isValid: boolean): void {
  //   if (isValid) {
  //     this.prepareToSave(formModel);
  //     if (this.entity.id) {
  //       this._service.update(this.entity).subscribe(
  //         () => {
  //           this.goBack();
  //           this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
  //         });
  //     } else {
  //       this._service.save(this.entity).subscribe(
  //         () => {
  //           this.goBack();
  //           this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
  //         });
  //     }
  //   }
  // }

  // private prepareToSave(formModel: Assunto): void {
  //   this.entity = { ...this.entity, ...formModel };
  // }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new Assunto(null);
  }

  public downloadArquivo(arquivo: IArquivo, download = true) {
    this._arquivoService.downloadFile(arquivo.hash, download);
  }
}
