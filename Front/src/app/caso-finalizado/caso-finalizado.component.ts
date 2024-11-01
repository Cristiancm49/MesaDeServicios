import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewTrazabilidadSolicitante } from '../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { MiHistorico } from '../interfaces/CasoFinalizado/MiHistorico';
import { Casofinalizado } from '../core/services/caso-finalizado.service';
import { Documento } from '../DatosLogin/User';

@Component({
  selector: 'app-caso-finalizado',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './caso-finalizado.component.html',
  styleUrl: './caso-finalizado.component.css'
})
export class CasoFinalizadoComponent {

  Historico: MiHistorico[] = [];
  vistatrazabilidad: ViewTrazabilidadSolicitante[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  selectedRowIndexP: number | null = null;

  constructor(private casofinalizado: Casofinalizado){}

  ngOnInit() {
    this.loadDatosIncidencia();
    this.loadDatosTrazabilidad(0);
  }

  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting Datos Incidencia...');
    
    this.casofinalizado.selectIncidenciaHistorica(Documento).subscribe({
      next: (data) => {
        this.Historico = data;
        this.isLoading = false;
        console.log('Datos Incidencia Solicitada:', this.Historico);
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
    
    this.casofinalizado.selectTrazabilidadSolicitada(selectedRowIndexP).subscribe({
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
    this.loadDatosTrazabilidad(this.selectedRowIndexP);
    }

}
