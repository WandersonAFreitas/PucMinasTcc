import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemSetorListComponent } from './fluxo-item-setor-list.component';

describe('FluxoItemSetorListComponent', () => {
  let component: FluxoItemSetorListComponent;
  let fixture: ComponentFixture<FluxoItemSetorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemSetorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemSetorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
