import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FluxoSituacaoListComponent } from './fluxo-situacao-list.component';


describe('FluxoSituacaoComponent', () => {
  let component: FluxoSituacaoListComponent;
  let fixture: ComponentFixture<FluxoSituacaoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FluxoSituacaoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FluxoSituacaoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
