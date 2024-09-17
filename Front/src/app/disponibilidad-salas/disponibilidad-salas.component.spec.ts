import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisponibilidadSalasComponent } from './disponibilidad-salas.component';

describe('DisponibilidadSalasComponent', () => {
  let component: DisponibilidadSalasComponent;
  let fixture: ComponentFixture<DisponibilidadSalasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisponibilidadSalasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisponibilidadSalasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
