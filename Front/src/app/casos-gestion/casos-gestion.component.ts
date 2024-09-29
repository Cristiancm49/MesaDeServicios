import { Component, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ViewIncidencia } from '../interfaces/CasoGestión/ViewIndicencia';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { ViewRoles } from '../interfaces/CasoGestión/ViewRoles';
import { RechazarIncidencia } from '../interfaces/CasoGestión/RechazarIncidencia';
import { MatDialog } from '@angular/material/dialog';
import { ModalAsignacionComponent } from '../Modales/modal-asignacion/modal-asignacion.component';
import { ModalPrioridadComponent } from '../Modales/modal-prioridad/modal-prioridad.component';
import { ModalRechazoComponent } from '../Modales/modal-rechazo/modal-rechazo.component';

@Component({
  selector: 'app-casos-gestion',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './casos-gestion.component.html',
  styleUrls: ['./casos-gestion.component.css']
})
export class CasosGestionComponent implements OnInit{
  vistadatos: ViewIncidencia[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  showNotification = false;
  notificationMessage = '';
  selectedRolId = 0;
  Pasarid = 0;
  isRowSelected: boolean = false;

  constructor(private casoGestion: CasoGestion, private cdr: ChangeDetectorRef, private _matDialog: MatDialog) { }


  abrirModalAsignacion(): void {
    this._matDialog.open(ModalAsignacionComponent, {
      width: '80%',
      maxWidth: '1000px',
      height: 'auto',
      maxHeight: '90vh',
      data: {valor: this.Pasarid}
    });
  }

  abrirModalPrioridad(): void {
    this._matDialog.open(ModalPrioridadComponent, {
      width: '80%',
      maxWidth: '500px',
      height: '80%',
      maxHeight: '500px',
      data: {valor: this.Pasarid}
    });
  }

  abrirModalRechazo(): void {
    this._matDialog.open(ModalRechazoComponent, {
      width: '80%',
      maxWidth: '500px',
      height: '80%',
      maxHeight: '500px',
      data: {valor: this.Pasarid}
    });
  }

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



  


  





  onRowSelect(index: number, inci_Id: number): void {
    this.selectedRowIndex = index;  // Usamos el índice para seleccionar la fila
    this.isRowSelected = true;  // Habilitar los botones cuando se selecciona una fila
    console.log(`Fila seleccionada: ${index}`);
    console.log(`Id de la incidencia seleccionada: ${inci_Id}`);

    this.Pasarid = inci_Id;
  }
  
}
