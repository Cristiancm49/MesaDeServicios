import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalPrioridadComponent } from './modal-prioridad.component';

describe('ModalPrioridadComponent', () => {
  let component: ModalPrioridadComponent;
  let fixture: ComponentFixture<ModalPrioridadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalPrioridadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalPrioridadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
