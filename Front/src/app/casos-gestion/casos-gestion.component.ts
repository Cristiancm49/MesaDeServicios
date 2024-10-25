import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ViewIncidencia } from '../interfaces/CasoGestión/ViewIndicencia';
import { viewpersonaoracle } from '../interfaces/CasoGestión/personaoracle';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalAsignacionComponent } from '../Modales/modal-asignacion/modal-asignacion.component';
import { ModalPrioridadComponent } from '../Modales/modal-prioridad/modal-prioridad.component';
import { ModalRechazoComponent } from '../Modales/modal-rechazo/modal-rechazo.component';
import { forkJoin, map } from 'rxjs';

@Component({
  selector: 'app-casos-gestion',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './casos-gestion.component.html',
  styleUrls: ['./casos-gestion.component.css']
})
export class CasosGestionComponent implements OnInit {
  vistadatos: (ViewIncidencia & Partial<viewpersonaoracle>)[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  showNotification = false;
  notificationMessage = '';
  selectedRolId = 0;
  Pasarid = 0;
  isRowSelected: boolean = false;

  constructor(
    private casoGestion: CasoGestion,
    private cdr: ChangeDetectorRef,
    private _matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadDatosIncidencia();
  }

  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.casoGestion.insertIncidencia().subscribe({
      next: (response) => {
        const incidencias = response.data || [];
        this.isLoading = false;
  
        const observables = incidencias.map((incidencia: ViewIncidencia) => 
          this.casoGestion.personaloracle(incidencia.cont_IdSolicitante).pipe(
            map(personaResponse => {
              const persona = personaResponse.data?.[0] || {};
              return {
                ...incidencia,
                nombreCompleto: persona.nombreCompleto || '',
                unid_Nombre: persona.unid_Nombre || '',
                tnom_Descripcion: persona.tnom_Descripcion || '',
              };
            })
          )
        );
  
        // Ejecutar todas las llamadas y asignar a vistadatos
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
  
  

  loadpersonal(contid: number): Promise<viewpersonaoracle | null> {
    return new Promise((resolve) => {
      this.casoGestion.personaloracle(contid).subscribe({
        next: (response) => {
          const personalData = response.data ? response.data[0] : null;
          resolve(personalData); // Retorna el primer elemento de `viewpersonaoracle`
        },
        error: (error) => {
          console.error('Error al cargar datos personales:', error);
          resolve(null); // Retorna null si hay un error
        }
      });
    });
  }

  abrirModalAsignacion(): void {
    this._matDialog.open(ModalAsignacionComponent, {
      width: '80%',
      maxWidth: '1000px',
      height: 'auto',
      maxHeight: '90vh',
      data: { valor: this.Pasarid }
    });
  }

  abrirModalPrioridad(): void {
    this._matDialog.open(ModalPrioridadComponent, {
      width: '80%',
      maxWidth: '500px',
      height: '80%',
      maxHeight: '500px',
      data: { valor: this.Pasarid }
    });
  }

  abrirModalRechazo(): void {
    this._matDialog.open(ModalRechazoComponent, {
      width: '80%',
      maxWidth: '500px',
      height: '80%',
      maxHeight: '500px',
      data: { valor: this.Pasarid }
    });
  }

  onRowSelect(index: number, inci_Id: number): void {
    this.selectedRowIndex = index;
    this.isRowSelected = true;
    console.log(`Fila seleccionada: ${index}`);
    console.log(`Id de la incidencia seleccionada: ${inci_Id}`);
    this.Pasarid = inci_Id;
  }
}
