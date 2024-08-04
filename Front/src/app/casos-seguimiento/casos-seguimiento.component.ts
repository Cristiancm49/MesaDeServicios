import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-casos-seguimiento',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './casos-seguimiento.component.html',
  styleUrl: './casos-seguimiento.component.css'
})
export class CasosSeguimientoComponent {
  mostrarDefault: boolean = true;
  mostrarEscalar: boolean = false;
  mostrarEditar: boolean = false;
  mostrarDevolver: boolean = false;

  constructor() { }

  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarEditar = seccion === 'editar';
    this.mostrarDevolver = seccion === 'devolver';
  }
}
