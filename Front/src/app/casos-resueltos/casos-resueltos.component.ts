import { Component, NgModule, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewIncidenciaSolicitada } from '../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { Casoactivo } from '../core/services/caso-activo.service';
import { Casoresuelto } from '../core/services/caso-resuelto.service';
import { UserDataStateService } from '../core/Datos/datos-usuario.service';
import { HttpResponse } from '@angular/common/http';
import { Documento } from '../DatosLogin/User';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-casos-resueltos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casos-resueltos.component.html',
  styleUrl: './casos-resueltos.component.css'
})
export class CasosResueltosComponent implements OnInit, OnDestroy {
  vistasolicitud: ViewIncidenciaSolicitada[] = [];
  vistatrazabilidad: ViewTrazabilidadSolicitante[] = [];
  DatosUsuario: DatosUser[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  selectedRowIndexP: number | null = null;
  private userDataSubscription: Subscription | undefined;


  constructor(
    private casoresuelto: Casoresuelto,
    private userDataState: UserDataStateService
  ){}

  ngOnInit() {
    this.setupUserData();  
  }

  ngOnDestroy(): void {

    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }


  private setupUserData(): void {
    this.userDataState.loading$.subscribe(
      isLoading => this.isLoading = isLoading
    );

    this.userDataSubscription = this.userDataState.userData$.subscribe({
      next: (data) => {
        if (data && data.length > 0) {
          this.DatosUsuario = data;
          console.log('Datos Usuario:', this.DatosUsuario);
          console.log('Pge:', this.DatosUsuario[0].cont_Id);
          
          this.loadDatosIncidencia();
        } else {
          console.error('No se encontraron datos de usuario');
        }
      },
      error: (error) => {
        console.error('Error en la suscripción de datos de usuario:', error);
      }
    });

    if (!this.userDataState.currentUserData) {
      this.userDataState.loadUserData(Documento);
    }
  }


  loadDatosIncidencia() {
    if (this.DatosUsuario.length > 0) {
      this.isLoading = true;
      console.log('Requesting Datos Incidencia...');
      
      this.casoresuelto.selectIncidenciaSolicitada(this.DatosUsuario[0].cont_Id).subscribe({
        next: (response) => {
          this.vistasolicitud = response.data || [];
          this.isLoading = false;
          console.log('Datos Incidencia Solicitada:', this.vistasolicitud);
        },
        error: (error) => {
          console.error('Sin Datos Incidencia Solicitada:', error);
          this.isLoading = false;
        }
      });
    } else {
      console.error('DatosUsuario está vacío o indefinido');
    }
  }

  loadDatosTrazabilidad(selectedRowIndexP: number) {
    this.isLoading = true;
    console.log('Requesting Datos Trazabilidad...');
    
    this.casoresuelto.selectTrazabilidadSolicitada(selectedRowIndexP).subscribe({
      next: (response) => {
        this.vistatrazabilidad = response.data || [];
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
