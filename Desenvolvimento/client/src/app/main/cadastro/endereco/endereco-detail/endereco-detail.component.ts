import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { EnderecoService } from '../endereco.service';
import { Endereco } from '@fuse/types/models/endereco';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { EmpresaEndereco } from '@fuse/types/models/empresa-endereco';
import { SetorEndereco } from '@fuse/types/models/setor-endereco';
import { MunicipioService } from '../../municipio/municipio.service';
import { LogradouroService } from '../../logradouro/logradouro.service';

@Component({
  selector: 'app-endereco-detail',
  templateUrl: './endereco-detail.component.html',
  styleUrls: ['./endereco-detail.component.scss']
})
export class EnderecoDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;
  private entity: Endereco;
  public gridSettingsModelMunicipio: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<EnderecoDetailComponent>,
    private _shellService: ShellService,
    private _fg: FormBuilder,
    private _service: EnderecoService,
    public _serviceMunicipio: MunicipioService,
    private _serviceLogradouro: LogradouroService,
    @Inject(MAT_DIALOG_DATA) public data: { endereco: Endereco, referenciaId: number, tipo: string }
  ) {
    this.initialize();
    this.id = this.data.endereco && this.data.endereco.id;
    
    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModelMunicipio = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
    
    this.createFormValidators();
  }

  private initialize(): void {
    this.entity = new Endereco(null);
  }

  ngOnInit() {
    this.entity = this.data.endereco;

    if (this.entity)
      this.createFormValidators();
  }

  private createFormValidators(): void {
    this.formGroup = this._fg.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      cep: new FormControl(this.entity.cep, [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
      logradouro: new FormControl(this.entity.logradouro, [Validators.required, Validators.minLength(3), Validators.maxLength(200)]),
      complemento: new FormControl(this.entity.complemento, [Validators.minLength(3), Validators.maxLength(200)]),
      bairro: new FormControl(this.entity.bairro, [Validators.minLength(3), Validators.maxLength(200)]),
      numero: new FormControl(this.entity.numero, [Validators.minLength(1), Validators.maxLength(20)]),
      municipioId: [this.entity && this.entity.municipioId],
      municipioNome: [this.entity.municipio && (this.entity.municipio.nome)],
    });
  }

  public save(formModel: Endereco, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._service.update(this.entity).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.entity).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Endereco): void {
    if (this.data.tipo == "Empresa")
    {
      let empresaEndereco = new EmpresaEndereco(null);
      empresaEndereco.empresaId = this.data.referenciaId;
      formModel.empresaEnderecos = new Array<EmpresaEndereco>();
      formModel.empresaEnderecos.push(empresaEndereco)
    }  
    else if (this.data.tipo == "Setor")
    {
      let setorEndereco = new SetorEndereco(null);
      setorEndereco.setorId = this.data.referenciaId;
      formModel.setorEnderecos = new Array<EmpresaEndereco>();
      formModel.setorEnderecos.push(setorEndereco)
    }  
    
    this.entity = { ...this.entity, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public GetLogradouro(): void {
    if (this.formGroup.controls.cep.value != ""){
      this._serviceLogradouro.getLogradouro(this.formGroup.controls.cep.value)
        .subscribe((data) => {
          if (data) {
            this.formGroup.controls.logradouro.setValue(data.endereco);
            this.formGroup.controls.bairro.setValue(data.bairro);
            this.formGroup.controls.municipioId.setValue(data.municipioId);
            this.formGroup.controls.municipioNome.setValue(data.municipio.nome);
          }
        });
    }
  }
}