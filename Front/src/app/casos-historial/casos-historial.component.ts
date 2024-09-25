import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Historico } from '../core/services/Historico.service';
import { HttpResponse } from '@angular/common/http';
import { ViewEncapsulation } from '@angular/core';
import { Documento } from '../DatosLogin/User';
import { TrazabilidadHistorico} from '../interfaces/HistorialIncidencias/ViewTrazabilidad';
import { ViewHistorico } from '../interfaces/HistorialIncidencias/ViewHistorico';

@Component({
  selector: 'app-casos-historial',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casos-historial.component.html',
  styleUrl: './casos-historial.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CasosHistorialComponent {

  vistahistorico: ViewHistorico[] = [];
  vistatrazabilidad: TrazabilidadHistorico[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;


  constructor(private historico: Historico){}

  ngOnInit() {
    this.loadDatosIncidencias();
    this.loadDatosTrazabilidad(0);

  }

  loadDatosIncidencias() {
    this.isLoading = true;
    console.log('Requesting Datos Incidencias...');
    
    this.historico.selectIncidenciaHistorica().subscribe({
      next: (data) => {
        this.vistahistorico = data;
        this.isLoading = false;
        console.log('Datos Incidencias: ', this.vistahistorico);
      },
      error: (error) => {
        console.error('Sin Datos de Incidencias:', error);
        this.isLoading = false;
      }
    });
  }

  loadDatosTrazabilidad(inci_id: number) {
    this.isLoading = true;
    console.log('Requesting Datos Trazabilidad...');
    
    this.historico.selectTrazabilidadSolicitada(inci_id).subscribe({
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

  verReporte(inci_Id: number) {
    console.log('ID de la incidencia seleccionada:', inci_Id);
    // Aquí puedes agregar la lógica para manejar el reporte,
    // como navegar a una página de detalle o ejecutar otro método.
    this.loadDatosTrazabilidad(inci_Id); // Ejemplo de uso del ID
  }

}
