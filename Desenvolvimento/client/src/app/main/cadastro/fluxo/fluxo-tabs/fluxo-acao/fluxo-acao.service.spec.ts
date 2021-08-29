import { TestBed } from '@angular/core/testing';

import { FluxoAcaoService } from './fluxo-acao.service';

describe('FluxoAcaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoAcaoService = TestBed.get(FluxoAcaoService);
    expect(service).toBeTruthy();
  });
});
