import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewIncidencia } from '../interfaces/ViewIndicencia';
import { CasoGestion } from '../core/services/caso-gestion';
import { ViewPersonalAsignacion } from '../interfaces/ViewPersonalAsignacion';
import { InsertAsignacion } from '../interfaces/Insert-Asignacion';

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
  vistapersonal: ViewPersonalAsignacion[] = [];
  insertarpersonal: InsertAsignacion[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  selectedRowIndexP: number | null = null;
  showNotification = false;
  notificationMessage = '';

  InsertAsignacion: InsertAsignacion = {

    inci_Id : 0,
    usua_Id : 0  
  }
  
  constructor(private casoGestion: CasoGestion) { }

  ngOnInit() {
    this.loadDatosIncidencia();
    this.loadDatosPersonal();
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

  loadDatosPersonal() {
    this.isLoading = true;
    console.log('Requesting DatosPersonal...');
    
    this.casoGestion.mostrarpersonal().subscribe({
      next: (data) => {
        this.vistapersonal = data;
        this.isLoading = false;
        console.log('Datos Personal:', this.vistapersonal);
      },
      error: (error) => {
        console.error('Sin datos de Personal:', error);
        this.isLoading = false;
      }
    });
  }

  onSubmit() {
    


    console.log('Valores capturados:');
    console.log('Id Incidencia:', this.InsertAsignacion.inci_Id);
    console.log('Id Personal:', this.InsertAsignacion.usua_Id);
    
    this.casoGestion.insertasignacion(this.InsertAsignacion).subscribe({
      next: (response) => {
        console.log('Incidencia asignada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia asignada con éxito';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al asignar la incidencia:', error);
        this.showNotification = true;
        this.notificationMessage = 'Error al asignar la incidencia:';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000);
      }
    });
  }


  onRowSelect(index: number, item: any): void {
    this.selectedRowIndex = index; // Actualiza el índice de la fila seleccionada
    console.log(`Fila seleccionada: ${index + 1}`);
    console.log('Datos de la fila:', this.vistadatos); // Muestra los datos de la fila
    this.InsertAsignacion.inci_Id = index + 1;
  }

  onRowSelectP(index: number, item: any): void {
    this.selectedRowIndexP = index; // Actualiza el índice de la fila seleccionada
    console.log(`Fila seleccionada: ${index + 1}`);
    console.log('Datos de la fila:', this.vistapersonal); // Muestra los datos de la fila
    this.InsertAsignacion.usua_Id = index + 1;
  }

  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarRechazar = seccion === 'rechazar';
  }
}
