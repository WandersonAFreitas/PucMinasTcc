import { TestBed } from '@angular/core/testing';

import { FluxoItemSetorService } from './fluxo-item-setor.service';

describe('FluxoItemSetorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoItemSetorService = TestBed.get(FluxoItemSetorService);
    expect(service).toBeTruthy();
  });
});
