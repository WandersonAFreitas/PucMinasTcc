import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageUserSetorComponent } from './manage-user-setor.component';

describe('ManageUserSetorComponent', () => {
  let component: ManageUserSetorComponent;
  let fixture: ComponentFixture<ManageUserSetorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageUserSetorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageUserSetorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
