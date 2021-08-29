import { Component, OnInit, ViewChild, Inject, ElementRef, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, } from '@angular/forms';
import { MatSelectionList, MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent, MatTableDataSource } from '@angular/material';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { IArquivo } from '@fuse/types/interfaces/i-arquivo';
import { Assunto } from '@fuse/types/models/assunto';
import { AssuntoService } from '@fuse/core/assunto.service';
import { SetorService } from 'app/main/cadastro/empresa/setor.service';
import { ShellService } from '@fuse/core/shell.service';
import { ArquivoService } from '@fuse/core/arquivo.service';


@Component({
  selector: 'empresa-tab-assuntos-detail',
  templateUrl: './assunto-detail.component.html',
  styleUrls: ['./assunto-detail.component.scss']
})
export class EmpresaTabAssuntoDetailComponent implements OnInit, AfterViewInit {


  public id: number;
  public formGroup: FormGroup;
  public gridSettingsModel: GridSettings;

  public displayedColumnsArquivos: string[] = ['ArquivoNome', 'Actions'];
  public dataSourceArquivos: MatTableDataSource<IArquivo>;
  public files = new Array<File>();
  public arquivos = new Array<IArquivo>();

  private assunto: Assunto;
  private empresaId: number;

  constructor(
    public thisDialogRef: MatDialogRef<EmpresaTabAssuntoDetailComponent>,
    private _service: AssuntoService,
    private _arquivoService: ArquivoService,
    public _setorService: SetorService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { assunto: Assunto, empresaId: number }
  ) {
    this.initialize();
    this.empresaId = this.data.empresaId;
    this.id = this.data.assunto && this.data.assunto.id;
    this.createFormValidators();
  }

  ngOnInit() {
    // this.initSetorVirtualSubject();
    this.assunto = this.data.assunto;
    if (this.assunto) {
      this.arquivos = this.assunto.assuntoArquivos.map(x => <IArquivo>{
        id: x.arquivo.id,
        nome: x.arquivo.nome,
        hash: x.arquivo.hash
      });
      this.dataSourceArquivos = new MatTableDataSource<IArquivo>(this.arquivos);
      this.createFormValidators();
    }
  }

  ngAfterViewInit() {
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.assunto.id, disabled: true }],
      empresaId: [this.empresaId],
      // TODO: REMOVER
      // setorProcessoFisicoId: [this.assunto.setorProcessoFisicoId],
      // setorProcessoFisicoNome: [this.assunto.setorProcessoFisico && (this.assunto.setorProcessoFisico.sigla + ' - ' + this.assunto.setorProcessoFisico.nome)],
      // setorProcessoVirtualId: [this.assunto.setorProcessoVirtualId],
      // setorProcessoVirtualNome: [this.assunto.setorProcessoVirtual && (this.assunto.setorProcessoVirtual.sigla + ' - ' + this.assunto.setorProcessoVirtual.nome)],
      ativo: [this.assunto.ativo],
      orientacoes: [this.assunto.orientacoes],
      nome: [this.assunto.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
    });
  }

  public save(formModel: Assunto, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.assunto.id) {
        this._service.update(this.assunto).subscribe(
          () => {
            this.salvarArquivos().then(x => {
              this.thisDialogRef.close('Ok');
              this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
            });
          });
      } else {
        this._service.save(this.assunto).subscribe(
          (assunto) => {
            this.id = assunto.id;
            this.salvarArquivos().then(x => {
              this.thisDialogRef.close('Ok');
              this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
            });
          });
      }
    }
  }

  private prepareToSave(formModel: Assunto): void {
    this.assunto = { ...this.assunto, ...formModel };
  }

  private initialize(): void {
    this.initGridSettingsModel();
    this.assunto = new Assunto(null);
  }

  private initGridSettingsModel() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public anexaArquivoInMemory(event: any) {
    if (event.target.files[0] !== undefined) {
      const file = event.target.files[0];
      this.files.push(file);
      const novoArquivo = <IArquivo>{
        nome: file.name
      };
      this.arquivos.push(novoArquivo);
      this.dataSourceArquivos = new MatTableDataSource<IArquivo>(this.arquivos);
    }
  }

  public async salvarArquivos() {
    if (this.files && this.files.length > 0) {
      const arquivoArray = await this._arquivoService.uploadFilesPromise(this.files, false, `assunto/${this.id}`);
      if (arquivoArray.length === this.files.length) {
        for (const arquivo of arquivoArray) {
          const documento = this.arquivos.find((doc, index, arr) => doc.nome === arquivo.nome);
          documento.hash = arquivo.hash;
        }
      } else {
        this._shellService.alert().error({
          timeout: 5000,
          messages: ['Ocorreu a falha em salvar ' + (this.arquivos.length - arquivoArray.length) + ' arquivo(s)']
        });
      }
    }
    return undefined;
  }

  public deletaArquivo(arquivo: IArquivo) {
    this._shellService.confirm().confirm({ message: `Deseja remover o modelo: "${arquivo.nome}" ?`, title: 'Remover modelo' })
      .subscribe(res => {
        if (res) {
          if (arquivo.id) {
            this._arquivoService.remove(arquivo.id, false).subscribe(() => {
              this.files.splice(this.files.indexOf(this.files.find((file, index, arr) => arquivo.nome === file.name)), 1);
              this.arquivos.splice(this.arquivos.indexOf(arquivo), 1);
              this.dataSourceArquivos = new MatTableDataSource<IArquivo>(this.arquivos);
            });
          } else {
            this.files.splice(this.files.indexOf(this.files.find((file, index, arr) => arquivo.nome === file.name)), 1);
            this.arquivos.splice(this.arquivos.indexOf(arquivo), 1);
            this.dataSourceArquivos = new MatTableDataSource<IArquivo>(this.arquivos);
          }
        }
      })
  }

  public downloadArquivo(arquivo: IArquivo, download = true) {
    this._arquivoService.downloadFile(arquivo.hash, download);
  }

}