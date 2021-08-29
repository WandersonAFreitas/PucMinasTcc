import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ViewChild, QueryList, ViewChildren } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { EmpresaService } from '../empresa.service';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Empresa } from '@fuse/types/models/empresa';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  templateUrl: './empresa-detail.component.html',
  styleUrls: ['./empresa-detail.component.css'],
})
export class EmpresaDetailComponent implements OnInit {

  public displayedColumns: string[] = ['Action', 'Nome', 'Sigla', 'Ativo'];
  public id: number;
  public formGroup: FormGroup;
  public expandedElement: any;

  public entity: Empresa = new Empresa(null);

  constructor(
    private _router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private _fb: FormBuilder,
    private _empresaService: EmpresaService,
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
          this._empresaService.get(this.id, false),
        ).pipe(
          finalize(() => this._shellService.unblockUI())
        ).subscribe(([entity]) => {
          this.entity = entity;
          this.entity.setores = this.entity.setores.filter(x => !x.setorPaiId);
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
      ativo: [this.entity.ativo],
      sigla: [this.entity.sigla, [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      nome: [this.entity.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      setores: this._fb.array([]),
    });
  }

  public save(formModel: Empresa, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._empresaService.update(this.entity).subscribe(
          () => {
            this.goBack();
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._empresaService.save(this.entity).subscribe(
          (empresa) => {
            this._router.navigate(['/cadastro/empresa/edit', empresa.id]);
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Empresa): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new Empresa(null);
  }

  public selectedIndexChange(event: number) {
    if (event === 0) {
      // this.closeAll();
    }
  }

  // public getRows(table: MatTable<any>, data: Array<any>) {
  //   if (data) {
  //     const rows = [];
  //     data.forEach((element: any) => rows.push(element, { detailRow: true, element }));
  //     return rows;
  //   }
  // }
}
