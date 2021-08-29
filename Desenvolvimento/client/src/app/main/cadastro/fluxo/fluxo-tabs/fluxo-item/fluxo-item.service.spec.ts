import { TestBed } from '@angular/core/testing';

import { FluxoItemService } from './fluxo-item.service';

describe('FluxoItemService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoItemService = TestBed.get(FluxoItemService);
    expect(service).toBeTruthy();
  });
});
