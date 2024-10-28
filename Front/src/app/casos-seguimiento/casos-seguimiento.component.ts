import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewSeguimiento } from '../interfaces/CasoSeguimiento/viewseguimiento';
import { Seguimiento } from '../core/services/Seguimiento.service';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { viewpersonaoracle } from '../interfaces/CasoGestión/personaoracle';
import { ViewReporte } from '../interfaces/CasoSeguimiento/Viewreportes';
import { Insertaceptacion } from '../interfaces/CasoSeguimiento/Aceptar';
import { forkJoin, map } from 'rxjs';

@Component({
  selector: 'app-casos-seguimiento',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casos-seguimiento.component.html',
  styleUrl: './casos-seguimiento.component.css'
})
export class CasosSeguimientoComponent implements OnInit{
  mostrarDefault: boolean = true;
  mostrarEscalar: boolean = false;
  mostrarEditar: boolean = false;
  mostrarDevolver: boolean = false;
  isLoading = true;
  vistaseguimiento: ViewSeguimiento[] = [];
  vistadatos: (ViewSeguimiento & Partial<viewpersonaoracle>)[] = [];
  vistareporte: ViewReporte[] = [];
  selectedRowIndexP: number | null = null;

  aceptado: Insertaceptacion = {
    inci_Id: 0
  }

  constructor(private casoseguimieto: Seguimiento,
    private casoGestion: CasoGestion
  ) { }

  ngOnInit() {
    this.loadDatosIncidencia();
  }


  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.casoseguimieto.selectseguimiento().subscribe({
      next: (response) => {
        const incidencias = response.data || [];
        this.isLoading = false;
  
        const observables = incidencias.map((incidencia: ViewSeguimiento) =>
          this.casoGestion.personaloracle(incidencia.contratoEncargado).pipe(
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

  loadReporte(Idince: number) {
    this.isLoading = true;
    console.log('Requesting Tipos de solución');
    this.casoseguimieto.selectdiagnostico(Idince).subscribe({
      next: (response) => {
        this.vistareporte = response.data || [];;
        this.isLoading = false;
        console.log('Datos Reporte Full:', this.vistareporte);
      },
      error: (error) => {
        console.error('Sin datos de Reportes:', error);
        this.isLoading = false;
      }
    });
  }

  onRowSelectP(inci_Id: number, item: any): void {
    this.selectedRowIndexP = inci_Id; // Guarda el usua_Id seleccionado
    console.log(`Usuario seleccionado con inci_Id: ${inci_Id}`);
    this.aceptado.inci_Id = this.selectedRowIndexP;
    this.loadReporte(this.selectedRowIndexP);
  }

  onSubmit() {
    this.casoseguimieto.insertDiagnostico(this.aceptado).subscribe({
      next: (response) => {
        console.log('Aceptación insertado con éxito:', response);
        setTimeout(() => {
          window.location.reload();
        }, 5000);
      },
      error: (error) => {
        console.error('Error al insertar la aceptación:', error);
      }
    });
  }

  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarEditar = seccion === 'editar';
    this.mostrarDevolver = seccion === 'devolver';
  }
}
