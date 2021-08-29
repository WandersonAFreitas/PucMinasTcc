import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FluxoAcaoService } from '../fluxo-acao.service';
import { FluxoAcao } from '@fuse/types/models/fluxo-acao';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { AcaoService } from 'app/main/cadastro/acao/acao.service';

@Component({
  selector: 'app-fluxo-acao-detail',
  templateUrl: './fluxo-acao-detail.component.html',
  styleUrls: ['./fluxo-acao-detail.component.scss']
})
export class FluxoAcaoDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoAcao: FluxoAcao;
  private fluxoId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoAcaoDetailComponent>,
    private _service: FluxoAcaoService,
    private _shellService: ShellService,
    private _acaoService: AcaoService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoAcao: FluxoAcao, fluxoId: number }
  ) {
    this.fluxoAcao = new FluxoAcao(null);
    this.fluxoId = this.data.fluxoId;
    this.id = this.data.fluxoAcao && this.data.fluxoAcao.id;

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoAcao = this.data.fluxoAcao;

    if (this.fluxoAcao) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.fluxoAcao.id, disabled: true }],
      nome: [this.fluxoAcao.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      fluxoId: [this.fluxoAcao.fluxoId ? this.fluxoAcao.fluxoId : this.fluxoId],
      acaoId: [this.fluxoAcao.fluxoId],
      unlock: [false]
    });
  }

  public save(formModel: FluxoAcao, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoAcao.id) {
        this._service.update(this.fluxoAcao).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoAcao).subscribe(
          (fluxoAcao) => {
            this.id = fluxoAcao.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoAcao): void {
    this.fluxoAcao = { ...this.fluxoAcao, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public onUnlock(): void {
    this.formGroup.controls['nome'].reset();
  }
}
