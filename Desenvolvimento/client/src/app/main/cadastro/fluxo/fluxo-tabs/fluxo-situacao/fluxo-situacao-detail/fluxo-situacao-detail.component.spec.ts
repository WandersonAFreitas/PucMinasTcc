import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoSituacaoDetailComponent } from './fluxo-situacao-detail.component';

describe('FluxoSituacaoDetailComponent', () => {
  let component: FluxoSituacaoDetailComponent;
  let fixture: ComponentFixture<FluxoSituacaoDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoSituacaoDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoSituacaoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
