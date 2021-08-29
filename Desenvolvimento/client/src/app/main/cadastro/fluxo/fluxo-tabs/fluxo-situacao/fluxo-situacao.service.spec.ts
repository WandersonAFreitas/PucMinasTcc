import { TestBed } from '@angular/core/testing';

import { FluxoSituacaoService } from './fluxo-situacao.service';

describe('FluxoSituacaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoSituacaoService = TestBed.get(FluxoSituacaoService);
    expect(service).toBeTruthy();
  });
});
