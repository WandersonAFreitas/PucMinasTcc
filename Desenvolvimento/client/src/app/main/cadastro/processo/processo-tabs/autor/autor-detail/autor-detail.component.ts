import { Component, OnInit, ViewChild, Inject, ElementRef, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, } from '@angular/forms';
import { MatSelectionList, MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent, MatTableDataSource } from '@angular/material';
import { ProcessoService } from '../../../processo.service';
import { Autor } from '@fuse/types/models/autor';
import { AutorService } from '@fuse/core/autor.service';
import { ProcessoAutorService } from '@fuse/core/processo-autor.service';
import { ShellService } from '@fuse/core/shell.service';
import { ProcessoAutor } from '@fuse/types/models/processo-autor';

@Component({
  selector: 'proceso-tab-autor-detail',
  templateUrl: './autor-detail.component.html',
  styleUrls: ['./autor-detail.component.css']
})
export class ProcessoTabAutorDetailComponent implements OnInit, AfterViewInit {

  public id: number;
  public formGroup: FormGroup;

  public displayedColumnsArquivos: string[] = ['ArquivoNome', 'Actions'];

  private autor: Autor;
  private processoId: number;

  constructor(
    public thisDialogRef: MatDialogRef<ProcessoTabAutorDetailComponent>,
    private _autorService: AutorService,
    private _processoAutorService: ProcessoAutorService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { autor: Autor, processoId: number }
  ) {
    this.initialize();
    this.processoId = this.data.processoId;
    this.id = this.data.autor && this.data.autor.id;
    this.createFormValidators();
  }

  ngOnInit() {
    this.autor = this.data.autor;
    this.createFormValidators();
  }

  ngAfterViewInit() {
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.autor && this.autor.id, disabled: true }],
      processoId: [this.processoId],
      nome: [this.autor && this.autor.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      cpfCnpj: [this.autor && this.autor.cpfCnpj, [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      email: [this.autor && this.autor.email, [Validators.required]],
      dataNascimento: [this.autor && this.autor.dataNascimento, [Validators.required]],
    });
  }

  public save(formModel: Autor, isValid: boolean): void {
    if (isValid) {
      console.log(formModel);

      this.prepareToSave(formModel);
      if (this.autor && this.autor.id) {
        this._autorService.update(this.autor).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Autor atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._autorService.save(this.autor).subscribe(
          (autor) => {
            this.id = autor.id;
            const autorId = this.id;
            const processoId = this.processoId;
            const newProcessoAutor = new ProcessoAutor(null);
            newProcessoAutor.autorId = autorId;
            newProcessoAutor.processoId = processoId;
            this._processoAutorService.save(newProcessoAutor).subscribe(
              () => {
                this.thisDialogRef.close('Ok');
                this._shellService.alert().success({ messages: ['Autor adicionado ao processo com sucesso!'], timeout: 3000 });
              });
          });
      }
    }
  }

  private prepareToSave(formModel: Autor): void {
    this.autor = { ...this.autor, ...formModel };
  }

  private initialize(): void {
    this.autor = new Autor(null);
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }
}
