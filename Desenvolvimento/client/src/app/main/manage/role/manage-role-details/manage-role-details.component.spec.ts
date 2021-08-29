import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageRoleDetailsComponent } from './manage-role-details.component';

describe('ManageRoleDetailsComponent', () => {
  let component: ManageRoleDetailsComponent;
  let fixture: ComponentFixture<ManageRoleDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageRoleDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageRoleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
