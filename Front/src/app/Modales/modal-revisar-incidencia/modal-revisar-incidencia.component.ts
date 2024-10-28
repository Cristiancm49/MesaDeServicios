import { Component, Inject, NgModule, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ViewSeguimiento } from '../../interfaces/CasoSeguimiento/viewseguimiento';
import { Seguimiento } from '../../core/services/Seguimiento.service';
import { viewpersonaoracle } from '../../interfaces/CasoGestión/personaoracle';
import { ViewReporte } from '../../interfaces/CasoSeguimiento/Viewreportes';
import { Insertaceptacion } from '../../interfaces/CasoSeguimiento/Aceptar';
import { ModalEscalarInternoComponent } from '../modal-escalar-interno/modal-escalar-interno.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-revisar-incidencia',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal-revisar-incidencia.component.html',
  styleUrl: './modal-revisar-incidencia.component.css'
})
export class ModalRevisarIncidenciaComponent {

  isLoading = true;
  vistaseguimiento: ViewSeguimiento[] = [];
  vistadatos: (ViewSeguimiento & Partial<viewpersonaoracle>)[] = [];
  vistareporte: ViewReporte[] = [];
  selectedRowIndexP: number | null = null;

  aceptado: Insertaceptacion = {
    inci_Id: 0
  }

  valorRecibido: any;
  conid: any;

  constructor(public matDialogRef: MatDialogRef<ModalRevisarIncidenciaComponent>, private casoseguimieto: Seguimiento, private _matDialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { valor: any}
) {
  this.valorRecibido = data.valor;
  this.aceptado.inci_Id= this.valorRecibido;
  console.log('dato recibido',this.valorRecibido);
}


  ngOnInit(): void {

    this.loadReporte(this.valorRecibido);
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

  onSubmit() {
    this.casoseguimieto.insertDiagnostico(this.aceptado).subscribe({
      next: (response) => {
        console.log('Aceptación insertado con éxito:', response);
      },
      error: (error) => {
        console.error('Error al insertar la aceptación:', error);
      }
    });
  }

  onAceptar(): void {
    this.onSubmit();
    console.log('Incidencia aceptada.');
    this.matDialogRef.close();
  }

  onEscalar(): void {
    console.log('Escalar incidencia.');
    this.abrirModalEscalar();
    // Lógica para abrir el modal de escalación.
  }

  abrirModalEscalar(): void {
    const dialogRef = this._matDialog.open(ModalEscalarInternoComponent, {
      width: '80%',
      maxWidth: '1000px',
      height: 'auto',
      maxHeight: '90vh',
      data: { valor: this.valorRecibido }
    });
  
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  onEditar(): void {
    console.log('Editar incidencia.');
    // Lógica para abrir el modal de edición.
  }

  onDevolver(): void {
    console.log('Devolver incidencia.');
    // Lógica para devolver la incidencia.
  }

  onCancel(): void {
    this.matDialogRef.close();
  }

}
