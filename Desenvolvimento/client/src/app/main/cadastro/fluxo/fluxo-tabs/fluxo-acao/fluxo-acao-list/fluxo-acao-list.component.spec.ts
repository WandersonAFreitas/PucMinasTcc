import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoAcaoListComponent } from './fluxo-acao-list.component';

describe('FluxoAcaoListComponent', () => {
  let component: FluxoAcaoListComponent;
  let fixture: ComponentFixture<FluxoAcaoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoAcaoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoAcaoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
