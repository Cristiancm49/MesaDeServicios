import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservasSalasComponent } from './reservas-salas.component';

describe('ReservasSalasComponent', () => {
  let component: ReservasSalasComponent;
  let fixture: ComponentFixture<ReservasSalasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservasSalasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservasSalasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
