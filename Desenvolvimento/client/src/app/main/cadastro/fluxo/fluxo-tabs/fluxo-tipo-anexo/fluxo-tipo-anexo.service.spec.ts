import { TestBed } from '@angular/core/testing';

import { FluxoTipoAnexoService } from './fluxo-tipo-anexo.service';

describe('FluxoTipoAnexoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoTipoAnexoService = TestBed.get(FluxoTipoAnexoService);
    expect(service).toBeTruthy();
  });
});
