import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoTipoAnexoListComponent } from './fluxo-tipo-anexo-list.component';

describe('FluxoTipoAnexoListComponent', () => {
  let component: FluxoTipoAnexoListComponent;
  let fixture: ComponentFixture<FluxoTipoAnexoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoTipoAnexoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoTipoAnexoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
