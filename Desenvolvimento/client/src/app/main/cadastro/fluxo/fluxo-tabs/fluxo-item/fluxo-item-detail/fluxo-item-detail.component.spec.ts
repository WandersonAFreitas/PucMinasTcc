import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FluxoItemDetailComponent } from './fluxo-item-detail.component';

describe('FluxoItemDetailComponent', () => {
  let component: FluxoItemDetailComponent;
  let fixture: ComponentFixture<FluxoItemDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoItemDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoItemDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
