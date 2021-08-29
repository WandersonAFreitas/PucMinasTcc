import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { EstadoService } from '../estado.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Estado } from '@fuse/types/models/estado';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-estado',
  templateUrl: './estado-list.component.html',
  styleUrls: ['./estado-list.component.css']
})
export class EstadoListComponent extends BaseListComponent<Estado> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome', 'Pais'];

  public entityFilter: Estado;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _estadoService: EstadoService
  ) {
    super(_router, _estadoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Id', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/estado/new']);
  }

  public edit(entity: Estado): void {
    this.baseEdit(['/cadastro/estado/edit', entity.id]);
  }

  public delete(entity: Estado): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Estado: ${entity.nome}?`,
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

  private initFilter(): Estado {
    const model: Estado = new Estado(null);
    return model;
  }
}
