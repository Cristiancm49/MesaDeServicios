import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CommonModule } from '@angular/common';
import { CasosGestionComponent } from './casos-gestion.component';

describe('CasosGestionComponent', () => {
  let component: CasosGestionComponent;
  let fixture: ComponentFixture<CasosGestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule], // Importa CommonModule si el componente lo necesita
      declarations: [CasosGestionComponent] // Declara el componente en el módulo de pruebas
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasosGestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
