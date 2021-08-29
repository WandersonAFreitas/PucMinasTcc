import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemChecklistDetailComponent } from './fluxo-item-checklist-detail.component';

describe('FluxoItemChecklistDetailComponent', () => {
  let component: FluxoItemChecklistDetailComponent;
  let fixture: ComponentFixture<FluxoItemChecklistDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemChecklistDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemChecklistDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
