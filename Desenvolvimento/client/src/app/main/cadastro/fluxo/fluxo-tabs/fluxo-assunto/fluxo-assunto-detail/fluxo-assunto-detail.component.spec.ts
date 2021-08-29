import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoAssuntoDetailComponent } from './fluxo-assunto-detail.component';

describe('FluxoAssuntoDetailComponent', () => {
  let component: FluxoAssuntoDetailComponent;
  let fixture: ComponentFixture<FluxoAssuntoDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoAssuntoDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoAssuntoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
