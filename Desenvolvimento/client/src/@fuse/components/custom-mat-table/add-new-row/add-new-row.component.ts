import { Component, OnInit, Inject, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Empresa } from '@fuse/types/models/empresa';
import { Setor } from '@fuse/types/models/setor';
import { SetorService } from 'app/main/cadastro/empresa/setor.service';
import { ShellService } from '@fuse/core/shell.service';

@Component({
    selector: 'add-new-row',
    templateUrl: './add-new-row.component.html',
    styleUrls: ['./add-new-row.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AddNewRowComponent implements OnInit {
    public titulo: string;
    public empresa: Empresa;
    public setorPai: Setor;
    public setorNovo: Setor;
    public setor = new Setor(null, null, null, null, true);
    public formGroup: FormGroup;

    constructor(
        private _setorService: SetorService,
        public thisDialogRef: MatDialogRef<AddNewRowComponent>,
        private _shellService: ShellService,
        private _fb: FormBuilder,
        @Inject(MAT_DIALOG_DATA) public data: { setorNovo: Setor, empresa: Empresa, setorPai: Setor }
    ) {
        this.createFormValidators();
    }

    ngOnInit() {
        this.initial(this.data.setorNovo);
    }

    private initial(setorNovo) {
        this.setorNovo = setorNovo;

        if (this.setorNovo) {
            this.setor = this.setorNovo;
            this.createFormValidators();
        }
        this.empresa = this.data.empresa;
        this.setorPai = this.data.setorPai;
        this.titulo = !this.setorPai ? (this.setorNovo ? 'Editar setor' : 'Adicionar um novo setor') : `Adicionar sub novo setor para ${this.setorPai.nome}`;
    }

    public save(formModel: Setor, isValid: boolean, close: boolean = true): void {
        if (isValid) {

            this.prepareToSave(formModel);

            if (this.setorNovo) {
                this._setorService.update(this.setor).subscribe(
                    (setor) => {
                        if (close)
                            this.thisDialogRef.close('Ok');

                        this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
                    });
            } else {
                this.setor.empresaId = this.empresa.id;
                this.setor.setorPaiId = this.setorPai ? this.setorPai.id : null;
                this._setorService.save(this.setor).subscribe(
                    (setor) => {
                        if (close)
                            this.thisDialogRef.close(setor.id);
                        else {
                            this.initial(setor);
                        }
                        this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
                    });
            }
        }
    }

    private createFormValidators(): void {
        this.formGroup = this._fb.group({
            id: [{ value: this.setor.id, disabled: true }],
            ativo: [this.setor.ativo],
            mesmoEnderecoDaEmpresa: [this.setor.mesmoEnderecoDaEmpresa],
            sigla: [this.setor.sigla, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]],
            nome: [this.setor.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
        });
    }

    private prepareToSave(formModel: Setor): void {
        this.setor = { ...this.setor, ...formModel };
    }

    public onCloseCancel() {
        if (this.setor)
            this.thisDialogRef.close(this.setor.id);
        else
            this.thisDialogRef.close('Cancel');
    }

    public adicionaNovoSetor(): void {

    }
}
