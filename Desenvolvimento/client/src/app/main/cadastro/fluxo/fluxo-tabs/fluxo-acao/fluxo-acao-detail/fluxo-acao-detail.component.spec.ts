import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoAcaoDetailComponent } from './fluxo-acao-detail.component';

describe('FluxoAcaoDetailComponent', () => {
  let component: FluxoAcaoDetailComponent;
  let fixture: ComponentFixture<FluxoAcaoDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoAcaoDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoAcaoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
