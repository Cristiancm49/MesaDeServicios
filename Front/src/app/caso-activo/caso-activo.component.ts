import { Component, NgModule, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ViewIncidenciaSolicitada } from '../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { Casoactivo } from '../core/services/caso-activo.service';
import { UserDataStateService } from '../core/Datos/datos-usuario.service';
import { EvaluarIncidencia } from '../interfaces/CasoActivo/EvaluarIndicencia';
import { HttpResponse } from '@angular/common/http';
import { ViewEncapsulation } from '@angular/core';
import { Documento } from '../DatosLogin/User';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-caso-activo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './caso-activo.component.html',
  styleUrl: './caso-activo.component.css',
  encapsulation: ViewEncapsulation.None
})
export class CasoActivoComponent implements OnInit, OnDestroy{

  vistasolicitud: ViewIncidenciaSolicitada[] = [];
  vistatrazabilidad: ViewTrazabilidadSolicitante[] = [];
  DatosUsuario: DatosUser[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  selectedRowIndexP: number | null = null;
  evaluacionHabilitada: boolean = false;
  notas = [1, 2, 3, 4, 5];
  preguntas: string[] = [
    'Tiempo de resolución del caso',
    'Claridad y efectividad de los procesos para la resolución del caso',
    'Te sientes satisfecho con la solución proporcionada',
    'Frecuencia y relevancia de las actualizaciones proporcionadas',
    'Usabilidad de la plataforma de soporte'
  ];
  private userDataSubscription: Subscription | undefined;

  evaluar: EvaluarIncidencia = {
    inci_Id: 0,
    enCa_Preg1: 0,
    enCa_Preg2: 0,
    enCa_Preg3: 0,
    enCa_Preg4: 0,
    enCa_Preg5: 0
  }


  constructor(
    private casoactivo: Casoactivo,
    private userDataState: UserDataStateService
  ){}

  ngOnInit() {
    this.setupUserData();  
    this.loadDatosTrazabilidad(0);
    this.ValidacionEvaluacion(0); 
    this.evaluacionHabilitada = false; 
  }

  ngOnDestroy(): void {

    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  onSubmit() {
    if (this.selectedRowIndexP !== null) {
      this.evaluar.inci_Id = this.selectedRowIndexP;
      console.log('Enviando evaluación:', this.evaluar);
      
      this.casoactivo.enviarEvaluacion(this.evaluar).subscribe({
        next: (response) => {
          console.log('Hola: ', response);
          setTimeout(() => {
            this.showNotification = true;
            this.notificationMessage = 'Evaluación enviada con éxito';
            window.location.reload();
          }, 5000);

        },
        error: (error) => {
          console.error('Error al enviar la evaluación', error);
          this.showNotification = true;
          this.notificationMessage = 'Error al enviar la evaluación';
        }
      });
    } else {
      console.error('No se ha seleccionado ninguna incidencia');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione una incidencia antes de enviar la evaluación';
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
          
          // Llamamos a loadDatosIncidencia después de que se carguen los datos de usuario
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
      
      this.casoactivo.selectIncidenciaSolicitada(this.DatosUsuario[0].cont_Id).subscribe({
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
    
    this.casoactivo.selectTrazabilidadSolicitada(selectedRowIndexP).subscribe({
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


  ValidacionEvaluacion(selectedRowIndexP: number) {
    this.isLoading = true;
    console.log('Cargando Respuesta Habilitación');
    
    this.casoactivo.ValidarIndicencia(selectedRowIndexP).subscribe({
      next: (response) => {
        let data: string | string[] = response.data;
        console.log('Datos recibidos:', response);
        console.log('Tipo de datos en data:', typeof data);
        
        if (Array.isArray(data)) {
          data = data[0];
        }

        this.evaluacionHabilitada = data.toLowerCase() === 'true';
        
        this.isLoading = false;
        console.log('Evaluación habilitada:', this.evaluacionHabilitada);
      },
      error: (error) => {
        console.error('Error al obtener datos de habilitación:', error);
        this.isLoading = false;
        this.evaluacionHabilitada = false; 
      }
    });
  }
  
  

  onRowSelectP(usua_Id: number, item: any): void {
    this.selectedRowIndexP = usua_Id; // Guarda el usua_Id seleccionado
    console.log(`Incidencia seleccionada: ${usua_Id}`);
    console.log('Datos de la incidencia:', item); 
    this.loadDatosTrazabilidad(this.selectedRowIndexP);
    this.ValidacionEvaluacion(this.selectedRowIndexP);
    this.evaluar.inci_Id = this.selectedRowIndexP;
    }


    onPreguntaSelected(event: any, pregunta: string) {
      const valor = parseInt(event.target.value) || 0;
      (this.evaluar as any)[pregunta] = valor;
      console.log(`Pregunta ${pregunta} seleccionada:`, valor);
    }
}
