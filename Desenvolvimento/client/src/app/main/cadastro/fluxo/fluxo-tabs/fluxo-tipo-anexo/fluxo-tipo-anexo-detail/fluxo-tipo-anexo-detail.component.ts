import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FluxoTipoAnexoService } from '../fluxo-tipo-anexo.service';
import { FluxoTipoAnexo } from '@fuse/types/models/fluxo-tipo-anexo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { TipoAnexoService } from 'app/main/cadastro/tipoanexo/tipoanexo.service';

@Component({
  selector: 'app-fluxo-tipo-anexo-detail',
  templateUrl: './fluxo-tipo-anexo-detail.component.html',
  styleUrls: ['./fluxo-tipo-anexo-detail.component.scss']
})
export class FluxoTipoAnexoDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoTipoAnexo: FluxoTipoAnexo;
  private fluxoId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoTipoAnexoDetailComponent>,
    private _service: FluxoTipoAnexoService,
    private _shellService: ShellService,
    private _tipoAnexoService: TipoAnexoService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoTipoAnexo: FluxoTipoAnexo, fluxoId: number }
  ) {
    this.fluxoTipoAnexo = new FluxoTipoAnexo(null);
    this.fluxoId = this.data.fluxoId;
    this.id = this.data.fluxoTipoAnexo && this.data.fluxoTipoAnexo.id;

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoTipoAnexo = this.data.fluxoTipoAnexo;

    if (this.fluxoTipoAnexo) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.fluxoTipoAnexo.id, disabled: true }],
      nome: [this.fluxoTipoAnexo.nome, [Validators.required, Validators.minLength(2), Validators.maxLength(300)]],
      fluxoId: [this.fluxoTipoAnexo.fluxoId ? this.fluxoTipoAnexo.fluxoId : this.fluxoId],
      tipoAnexoId: [this.fluxoTipoAnexo.fluxoId],
      unlock: [false]
    });
  }

  public save(formModel: FluxoTipoAnexo, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoTipoAnexo.id) {
        this._service.update(this.fluxoTipoAnexo).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoTipoAnexo).subscribe(
          (fluxoTipoAnexo) => {
            this.id = fluxoTipoAnexo.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoTipoAnexo): void {
    this.fluxoTipoAnexo = { ...this.fluxoTipoAnexo, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public onUnlock(): void {
    this.formGroup.controls['nome'].reset();
  }
}
