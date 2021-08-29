import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemChecklistListComponent } from './fluxo-item-checklist-list.component';

describe('FluxoItemChecklistListComponent', () => {
  let component: FluxoItemChecklistListComponent;
  let fixture: ComponentFixture<FluxoItemChecklistListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemChecklistListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemChecklistListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
