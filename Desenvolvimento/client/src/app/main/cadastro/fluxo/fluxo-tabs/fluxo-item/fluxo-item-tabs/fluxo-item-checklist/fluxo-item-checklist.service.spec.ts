import { TestBed } from '@angular/core/testing';

import { FluxoItemChecklistService } from './fluxo-item-checklist.service';

describe('FluxoItemChecklistService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FluxoItemChecklistService = TestBed.get(FluxoItemChecklistService);
    expect(service).toBeTruthy();
  });
});
