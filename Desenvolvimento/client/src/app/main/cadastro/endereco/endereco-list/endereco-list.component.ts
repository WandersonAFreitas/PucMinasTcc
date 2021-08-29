import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { EnderecoService } from '../endereco.service';
import { MatDialog } from '@angular/material';
import { EnderecoDetailComponent } from '../endereco-detail/endereco-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Endereco } from '@fuse/types/models/endereco';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-endereco-list',
  templateUrl: './endereco-list.component.html',
  styleUrls: ['./endereco-list.component.css']
})
export class EnderecoListComponent extends BaseListComponent<Endereco> implements OnInit {

  @Input() public referenciaId: number = null;
  @Input() public tipo: string = null;
  
  public displayedColumns: string[] = ['Actions', 'Id', 'CEP', 'Logradouro', 'Numero'];
  public entityFilter: Endereco;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _enderecoService: EnderecoService
  ) {
    super(_router, _enderecoService, _shellService);
  }
  
  ngOnInit(): void {   
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'nome', 'asc');
    
    this.baseLoad(this.gridSettingsModel);
  }

  public delete(entity: Endereco): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Endereço: ${entity.logradouro}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
        });
      }
    });
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): Endereco {
    const model: Endereco = new Endereco(null);
    return model;
  }

  private initialFilter(): Filter {
    let rules = [];
    if (this.tipo == 'Empresa') {
      rules = [new Rule('empresaEnderecos.empresaId', this.op.eq, '' + this.referenciaId)];
    }
    else if (this.tipo == 'Setor') {
      rules = [new Rule('setorEnderecos.setorId', this.op.eq, '' + this.referenciaId)];
    }

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public add(): void {
    const referenciaId = this.referenciaId;
    const tipo = this.tipo;

    const dialogRef = this._dialog.open(EnderecoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        referenciaId,
        tipo
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(endereco: Endereco): void {
    const referenciaId = this.referenciaId;
    const tipo = this.tipo;

    const dialogRef = this._dialog.open(EnderecoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        endereco,
        referenciaId,
        tipo
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  private afterClosedRefreshGrids(dialogRef: any) {
    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Ok') {
        this.search();
      }
    });
  }
}
