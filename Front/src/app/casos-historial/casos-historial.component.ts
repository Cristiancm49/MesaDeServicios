import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Historico } from '../core/services/Historico.service';
import { HttpResponse } from '@angular/common/http';
import { ViewEncapsulation } from '@angular/core';
import { Documento } from '../DatosLogin/User';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { TrazabilidadHistorico} from '../interfaces/HistorialIncidencias/ViewTrazabilidad';
import { ViewHistorico } from '../interfaces/HistorialIncidencias/ViewHistorico';
import { viewpersonaoracle } from '../interfaces/CasoGestión/personaoracle';
import { forkJoin, map } from 'rxjs';

@Component({
  selector: 'app-casos-historial',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casos-historial.component.html',
  styleUrl: './casos-historial.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CasosHistorialComponent {

  vistatrazabilidad: TrazabilidadHistorico[] = [];
  vistadatos: (ViewHistorico & Partial<viewpersonaoracle>)[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;


  constructor(private historico: Historico,
    private casoGestion: CasoGestion
  ){}

  ngOnInit() {
    this.loadDatosIncidencia();
    this.loadDatosTrazabilidad(0);
  }

  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.historico.selectIncidenciaHistorica().subscribe({
      next: (response) => {
        const incidencias = response.data || [];
        this.isLoading = false;
  
        const observables = incidencias.map((incidencia: ViewHistorico) =>
          this.casoGestion.personaloracle(incidencia.idContratoSolicitante).pipe(
            map(personaResponse => {
              const persona = personaResponse.data?.[0] || {};
              return {
                ...incidencia,
                nombreCompleto: persona.nombreCompleto || '',
                tnom_Descripcion: persona.tnom_Descripcion || ''
              };
            })
          )
        );
  
        forkJoin(observables).subscribe({
          next: (result) => {
            this.vistadatos = result;
            console.log('Datos combinados:', this.vistadatos);
          },
          error: (error) => console.error('Error al combinar datos:', error)
        });
      },
      error: (error) => {
        console.error('Sin datos de Incidencia:', error);
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
