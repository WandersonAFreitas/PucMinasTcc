import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoAssuntoListComponent } from './fluxo-assunto-list.component';

describe('FluxoAssuntoListComponent', () => {
  let component: FluxoAssuntoListComponent;
  let fixture: ComponentFixture<FluxoAssuntoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoAssuntoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoAssuntoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
