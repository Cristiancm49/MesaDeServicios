import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewIncidencia } from '../interfaces/CasoGestión/ViewIndicencia';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { ViewPersonalAsignacion } from '../interfaces/CasoGestión/ViewPersonalAsignacion';
import { InsertAsignacion } from '../interfaces/CasoGestión/Insert-Asignacion';
import { ViewRoles } from '../interfaces/CasoGestión/ViewRoles';
import { RechazarIncidencia } from '../interfaces/CasoGestión/RechazarIncidencia';

@Component({
  selector: 'app-casos-gestion',
  standalone: true,
  imports: [CommonModule, FormsModule], 
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
  vistaroles: ViewRoles[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  selectedRowIndexP: number | null = null;
  showNotification = false;
  notificationMessage = '';
  selectedRolId = 0;

  InsertAsignacion: InsertAsignacion = {

    inci_Id : 0,
    usua_Id : 0  
  }

  RechazarIncidencia: RechazarIncidencia={

    inci_Id: 0,
    inTr_FechaActualizacion: new Date(),
    inTr_MotivoRechazo: ''

  }
  
  constructor(private casoGestion: CasoGestion) { }

  ngOnInit() {
    this.loadDatosIncidencia();
    this.loadDatosPersonal(0);
    this.loadDatosRol();
    this.cargarFechaHora();
    
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

  cargarFechaHora() {
    const now = new Date();
    this.RechazarIncidencia.inTr_FechaActualizacion = now;
    console.log("Fecha de rechazo", this.RechazarIncidencia.inTr_FechaActualizacion)
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

  onRechazar() {
    if (this.RechazarIncidencia.inci_Id === 0) {
      console.error('No se ha seleccionado ninguna incidencia');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione una incidencia antes de rechazar';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    if (!this.RechazarIncidencia.inTr_MotivoRechazo) {
      console.error('No se ha proporcionado un motivo de rechazo');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, proporcione un motivo de rechazo';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    console.log("Datos de rechazo a enviar:", this.RechazarIncidencia);
    
    this.casoGestion.rechazarinciden(this.RechazarIncidencia).subscribe({
      next: (response) => {
        console.log('Incidencia rechazada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia rechazada con éxito';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al rechazar la incidencia:', error);
        this.showNotification = true;
        this.notificationMessage = `Error al rechazar la incidencia: ${error.message || 'Desconocido'}`;
        setTimeout(() => {
          this.showNotification = false;
        }, 5000);
      }
    });
  }

  onRolSelected(event: any) {
    this.selectedRolId = parseInt(event.target.value) || 0;
    console.log('Categoría seleccionada:', this.selectedRolId);
    this.loadDatosPersonal(this.selectedRolId);
  }

  onRowSelect(index: number, inci_Id: number): void {
    this.selectedRowIndex = index; // Usamos el índice para seleccionar la fila
    console.log(`Fila seleccionada: ${index}`);
    console.log(`Id de la incidencia seleccionada: ${inci_Id}`);
    this.InsertAsignacion.inci_Id = inci_Id; 
    this.RechazarIncidencia.inci_Id = inci_Id;
  }
  

  onRowSelectP(usua_Id: number, item: any): void {
  this.selectedRowIndexP = usua_Id; // Guarda el usua_Id seleccionado
  console.log(`Usuario seleccionado con usua_Id: ${usua_Id}`);
  console.log('Datos del usuario seleccionado:', item); // Muestra los datos del usuario seleccionado
  this.InsertAsignacion.usua_Id = usua_Id; // Asigna el usua_Id a InsertAsignacion sin modificarlo
  }


  mostrarSeccion(seccion: string): void {
    this.mostrarDefault = seccion === 'default';
    this.mostrarEscalar = seccion === 'escalar';
    this.mostrarRechazar = seccion === 'rechazar';
  }
}
