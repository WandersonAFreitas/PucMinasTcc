import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemSetorDetailComponent } from './fluxo-item-setor-detail.component';

describe('FluxoItemSetorDetailComponent', () => {
  let component: FluxoItemSetorDetailComponent;
  let fixture: ComponentFixture<FluxoItemSetorDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemSetorDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemSetorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
