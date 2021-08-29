import { TestBed } from '@angular/core/testing';
import { FluxoItemAnexoService } from './fluxo-item-anexo.service';

describe('FluxoItemAnexoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoItemAnexoService = TestBed.get(FluxoItemAnexoService);
    expect(service).toBeTruthy();
  });
});
