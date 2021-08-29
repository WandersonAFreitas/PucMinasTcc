import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageUserRoleComponent } from './manage-user-role.component';

describe('ManageUserRoleComponent', () => {
  let component: ManageUserRoleComponent;
  let fixture: ComponentFixture<ManageUserRoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageUserRoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageUserRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
