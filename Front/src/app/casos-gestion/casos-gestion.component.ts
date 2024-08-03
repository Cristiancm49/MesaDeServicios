import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-casos-gestion',
  standalone: true,
  imports: [CommonModule], // Importar CommonModule aquí
  templateUrl: './casos-gestion.component.html',
  styleUrls: ['./casos-gestion.component.css']
})
export class CasosGestionComponent {
  mostrarDefault: boolean = true;
  mostrarEscalar: boolean = false;
  mostrarRechazar: boolean = false;

  constructor() { }

  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarRechazar = seccion === 'rechazar';
  }
}
