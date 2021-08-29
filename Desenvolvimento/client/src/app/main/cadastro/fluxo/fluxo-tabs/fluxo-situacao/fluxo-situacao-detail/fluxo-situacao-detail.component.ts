import { Component, OnInit, Inject } from '@angular/core';
import { FluxoSituacaoService } from '../fluxo-situacao.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FluxoSituacao } from '@fuse/types/models/fluxo-situacao';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { SituacaoService } from 'app/main/cadastro/situacao/situacao.service';

@Component({
  selector: 'app-fluxo-situacao-detail',
  templateUrl: './fluxo-situacao-detail.component.html',
  styleUrls: ['./fluxo-situacao-detail.component.scss']
})
export class FluxoSituacaoDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoSituacao: FluxoSituacao;
  private fluxoId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoSituacaoDetailComponent>,
    private _service: FluxoSituacaoService,
    private _shellService: ShellService,
    private _situacaoService: SituacaoService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoSituacao: FluxoSituacao, fluxoId: number }
  ) {
    this.fluxoSituacao = new FluxoSituacao(null);
    this.fluxoId = this.data.fluxoId;
    this.id = this.data.fluxoSituacao && this.data.fluxoSituacao.id;

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoSituacao = this.data.fluxoSituacao;
    if (this.fluxoSituacao) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [this.fluxoSituacao.id, { value: this.fluxoSituacao.id, disabled: true }],
      nome: [this.fluxoSituacao.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      fluxoId: [this.fluxoSituacao.fluxoId ? this.fluxoSituacao.fluxoId : this.fluxoId],
      situacaoId: [this.fluxoSituacao.fluxoId],
      unlock: [false]
    });
  }

  public save(formModel: FluxoSituacao, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoSituacao.id) {
        this._service.update(this.fluxoSituacao).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoSituacao).subscribe(
          (fluxoSituacao) => {
            this.id = fluxoSituacao.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoSituacao): void {
    this.fluxoSituacao = { ...this.fluxoSituacao, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public onUnlock(): void {
    this.formGroup.controls['nome'].reset();
  }
}
