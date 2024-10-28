import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalRevisarIncidenciaComponent } from './modal-revisar-incidencia.component';

describe('ModalRevisarIncidenciaComponent', () => {
  let component: ModalRevisarIncidenciaComponent;
  let fixture: ComponentFixture<ModalRevisarIncidenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalRevisarIncidenciaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalRevisarIncidenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
