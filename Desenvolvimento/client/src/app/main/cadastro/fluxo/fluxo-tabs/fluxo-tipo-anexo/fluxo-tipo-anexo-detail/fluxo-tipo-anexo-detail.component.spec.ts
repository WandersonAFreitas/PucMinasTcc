import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoTipoAnexoDetailComponent } from './fluxo-tipo-anexo-detail.component';

describe('FluxoTipoAnexoDetailComponent', () => {
  let component: FluxoTipoAnexoDetailComponent;
  let fixture: ComponentFixture<FluxoTipoAnexoDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoTipoAnexoDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoTipoAnexoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
