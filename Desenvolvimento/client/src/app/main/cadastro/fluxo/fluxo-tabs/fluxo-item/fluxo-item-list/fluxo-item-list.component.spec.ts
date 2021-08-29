import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemListComponent } from './fluxo-item-list.component';

describe('FluxoItemListComponent', () => {
  let component: FluxoItemListComponent;
  let fixture: ComponentFixture<FluxoItemListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
