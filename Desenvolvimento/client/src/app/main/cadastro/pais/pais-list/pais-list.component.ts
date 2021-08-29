import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PaisService } from '../pais.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Pais } from '@fuse/types/models/pais';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-pais',
  templateUrl: './pais-list.component.html',
  styleUrls: ['./pais-list.component.css']
})
export class PaisListComponent extends BaseListComponent<Pais> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: Pais;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _paisService: PaisService
  ) {
    super(_router, _paisService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Id', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/pais/new']);
  }

  public edit(entity: Pais): void {
    this.baseEdit(['/cadastro/pais/edit', entity.id]);
  }

  public delete(entity: Pais): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Pais: ${entity.nome}?`,
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

  private initFilter(): Pais {
    const model: Pais = new Pais(null);
    return model;
  }
}
