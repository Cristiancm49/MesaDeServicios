import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewIncidenciaSolicitada } from '../interfaces/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../interfaces/Trazabilidad-Solicitante';
import { Casoactivo } from '../core/services/caso-activo.service';

@Component({
  selector: 'app-caso-activo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './caso-activo.component.html',
  styleUrl: './caso-activo.component.css'
})
export class CasoActivoComponent {

  vistasolicitud: ViewIncidenciaSolicitada[] = [];
  vistatrazabilidad: ViewTrazabilidadSolicitante[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  loginusuario = 1004446325
  selectedRowIndexP: number | null = null;

  constructor(private casoactivo: Casoactivo){}

  ngOnInit() {
    this.loadDatosIncidencia();
    this.loadDatosTrazabilidad(0);
  }


  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting Datos Incidencia...');
    
    this.casoactivo.selectIncidenciaSolicitada(this.loginusuario).subscribe({
      next: (data) => {
        this.vistasolicitud = data;
        this.isLoading = false;
        console.log('Datos Incidencia Solicitada:', this.vistasolicitud);
      },
      error: (error) => {
        console.error('Sin Datos Incidencia Solicitada:', error);
        this.isLoading = false;
      }
    });
  }

  loadDatosTrazabilidad(selectedRowIndexP: number) {
    this.isLoading = true;
    console.log('Requesting Datos Trazabilidad...');
    
    this.casoactivo.selectTrazabilidadSolicitada(selectedRowIndexP).subscribe({
      next: (data) => {
        this.vistatrazabilidad = data;
        this.isLoading = false;
        console.log('Datos Trazabilidad:', this.vistatrazabilidad);
      },
      error: (error) => {
        console.error('Sin Datos de Trazabilidad:', error);
        this.isLoading = false;
      }
    });
  }

  onRowSelectP(usua_Id: number, item: any): void {
    this.selectedRowIndexP = usua_Id; // Guarda el usua_Id seleccionado
    console.log(`Incidencia seleccionada: ${usua_Id}`);
    console.log('Datos de la incidencia:', item); 
    this.loadDatosTrazabilidad(this.selectedRowIndexP);// Muestra los datos del usuario seleccionado el usua_Id a InsertAsignacion sin modificarlo
    }
}
