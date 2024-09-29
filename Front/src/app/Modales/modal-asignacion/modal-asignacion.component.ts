import { Component, NgModule, OnInit, Inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CasoGestion } from '../../core/services/caso-gestion.service';
import {ViewRoles} from '../../interfaces/CasoGestión/ViewRoles'
import { ViewPersonalAsignacion } from '../../interfaces/CasoGestión/ViewPersonalAsignacion';
import { InsertAsignacion } from '../../interfaces/CasoGestión/Insert-Asignacion';
import { MatDialogActions } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-asignacion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal-asignacion.component.html',
  styleUrl: './modal-asignacion.component.css'
})
export class ModalAsignacionComponent {
  mostrarDefault: boolean = true;
  mostrarPrioridad: boolean = false;
  mostrarRechazar: boolean = false;
  //vistadatos: ViewIncidencia[] = [];
  vistapersonal: ViewPersonalAsignacion[] = [];
  insertarpersonal: InsertAsignacion[] = [];
  vistaroles: ViewRoles[] = [];
  //vistraprioridad: SelectPrioridad[] = [];
  selectedPrioridadId = 0;
  //cambioprioridad: CambioPrioridad[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  selectedRowIndexP: number | null = null;
  showNotification = false;
  notificationMessage = '';
  selectedRolId = 0;
  activeTab: string = 'gestion';

  InsertAsignacion: InsertAsignacion = {

    inci_Id : 0,
    usua_Id : 0  
  }

  valorRecibido: any;
  constructor(
    public matDialogRef: MatDialogRef<ModalAsignacionComponent>, private casoGestion: CasoGestion,
    @Inject(MAT_DIALOG_DATA) public data: { valor: any }
  ) {
    this.valorRecibido = data.valor;
    console.log('dato recibido',this.valorRecibido);
    this.InsertAsignacion.inci_Id = this.valorRecibido;
  }
  

  ngOnInit() {
    this.loadDatosPersonal(0);
    this.loadDatosRol();
    
    //this.cargarFechaHora();
    //this.loadPrioridades();
  }

  onSubmit() {
    
    console.log('Valores para delegar:');
    console.log('Id Incidencia:', this.InsertAsignacion.inci_Id);
    console.log('Id Personal:', this.InsertAsignacion.usua_Id);
    
    this.casoGestion.insertasignacion(this.InsertAsignacion).subscribe({
      next: (response) => {
        console.log('Incidencia asignada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia asignada con éxito';
        setTimeout(() => {
          this.showNotification = false;
          window.location.reload();
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al asignar la incidencia:', error);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia asignada con éxito:';
        setTimeout(() => {
          this.showNotification = false;
          window.location.reload();
        }, 5000);
      }
    });
  }

  onCancel(): void {
    // Cierra el modal sin hacer cambios
    this.matDialogRef.close() 
  }

  loadDatosPersonal(selectedRolId: number) {
    this.isLoading = true;
    console.log('Requesting DatosPersonal...');
    
    this.casoGestion.mostrarpersonal(selectedRolId).subscribe({
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

  loadDatosRol() {
    this.isLoading = true;
    console.log('Requesting Datos Rol...');
    
    this.casoGestion.getRoles().subscribe({
      next: (data) => {
        this.vistaroles = data;
        this.isLoading = false;
        console.log('Datos Rol:', this.vistaroles);
      },
      error: (error) => {
        console.error('Sin datos de Rol:', error);
        this.isLoading = false;
      }
    });
  }

  onRolSelected(event: any) {
    this.selectedRolId = parseInt(event.target.value) || 0;
    console.log('Categoría seleccionada:', this.selectedRolId);
    this.loadDatosPersonal(this.selectedRolId);
  }

  onRowSelectP(usua_Id: number, item: any): void {
    this.selectedRowIndexP = usua_Id; // Guarda el usua_Id seleccionado
    console.log(`Usuario seleccionado con usua_Id: ${usua_Id}`);
    console.log('Datos del usuario seleccionado:', item); // Muestra los datos del usuario seleccionado
    this.InsertAsignacion.usua_Id = usua_Id; // Asigna el usua_Id a InsertAsignacion sin modificarlo
    }
}
