import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewIncidencia } from '../interfaces/ViewIndicencia';
import { CasoGestion } from '../core/services/caso-gestion';

@Component({
  selector: 'app-casos-gestion',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './casos-gestion.component.html',
  styleUrls: ['./casos-gestion.component.css']
})
export class CasosGestionComponent {
  mostrarDefault: boolean = true;
  mostrarEscalar: boolean = false;
  mostrarRechazar: boolean = false;
  vistadatos: ViewIncidencia[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;

  constructor(private casoGestion: CasoGestion) { }

  ngOnInit() {
    this.loadDatosIncidencia();
  }


  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.casoGestion.insertIncidencia().subscribe({
      next: (data) => {
        this.vistadatos = data;
        this.isLoading = false;
        console.log('Datos Incidencia:', this.vistadatos);
      },
      error: (error) => {
        console.error('Sin datos de Incidencia:', error);
        this.isLoading = false;
      }
    });
  }

  onRowSelect(index: number, item: any): void {
    this.selectedRowIndex = index; // Actualiza el índice de la fila seleccionada
    console.log(`Fila seleccionada: ${index + 1}`);
    console.log('Datos de la fila:', this.vistadatos); // Muestra los datos de la fila
  }

  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarRechazar = seccion === 'rechazar';
  }
}
