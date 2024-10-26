import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { Directive, ElementRef, AfterViewInit } from '@angular/core';
import { Casodelegado } from '../core/services/caso-delegados.service';
import { ViewIncidenciaAsignada } from '../interfaces/CasoDelegado/ViewIncidenciaAsignada';
import { ViewTipoSoluciones } from '../interfaces/CasoDelegado/ViewTipoSoluciones';
import { Documento } from '../DatosLogin/User';
import { InsertDiagnostico } from '../interfaces/CasoDelegado/InsertDiagnostico';
import { ViewEncapsulation } from '@angular/core';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { viewpersonaoracle } from '../interfaces/CasoGestión/personaoracle';
import { UserDataStateService } from '../core/Datos/datos-usuario.service';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { forkJoin, map, Subscription } from 'rxjs';

@Component({
  selector: 'app-casos-delegados',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casos-delegados.component.html',
  styleUrl: './casos-delegados.component.css',
  encapsulation: ViewEncapsulation.None
})


export class CasosDelegadosComponent {

  vistadatos: (ViewIncidenciaAsignada & Partial<viewpersonaoracle>)[] = [];
  vistaasignada: ViewIncidenciaAsignada[] = [];
  Tiposolucion: ViewTipoSoluciones[] = [];
  DatosUsuario: DatosUser[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  selectedRowIndexP: number | null = null;
  fechaHoraString: string = '';
  intervalo: any;
  selectedTipoId = 0;
  casoSolucionado: boolean | null = null;
  escalable: boolean | null = null;
  resolucionCaso: string = '';
  private userDataSubscription: Subscription | undefined;

  diagnotico: InsertDiagnostico = {
    inci_Id: 0,
    idContratoUsuario: 0,
    diag_DescripcionDiagnostico: '',
    diag_Solucionado: true,
    tiSo_Id: 0,
    diag_Escalable: true
  }


  constructor(private casodelegado: Casodelegado,  private cdr: ChangeDetectorRef, private userDataState: UserDataStateService, private casoGestion: CasoGestion,) { }

  ngOnInit() {
    this.setupUserData();
    this.loadDatosIncidencia();
    this.cargarFechaHora();
    this.loadTipoSolucion();

    this.intervalo = setInterval(() => {
      this.cargarFechaHora();
    }, 10000);
  }

  ngOnDestroy(): void {
    if (this.intervalo) {
      clearInterval(this.intervalo);
    }

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
          this.loadDatosIncidencia();
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
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.casodelegado.selectIncidenciaasignada(this.DatosUsuario[0].cont_Id).subscribe({
      next: (response) => {
        const incidencias = response.data || [];
        this.isLoading = false;
  
        const observables = incidencias.map((incidencia: ViewIncidenciaAsignada) =>
          this.casoGestion.personaloracle(incidencia.contratoSolicitante).pipe(
            map(personaResponse => {
              const persona = personaResponse.data?.[0] || {};
              return {
                ...incidencia,
                nombreCompleto: persona.nombreCompleto || '',
                tnom_Descripcion: persona.tnom_Descripcion || '',
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


  loadTipoSolucion() {
    this.isLoading = true;
    console.log('Requesting Tipos de solución');
    this.casodelegado.TipoS().subscribe({
      next: (response) => {
        this.Tiposolucion = response.data || [];;
        this.isLoading = false;
        console.log('Datos Tipos Full:', this.Tiposolucion);
      },
      error: (error) => {
        console.error('Sin datos de Tipos de solución:', error);
        this.isLoading = false;
      }
    });
  }

  onTipoSolucionSelected(event: any) {
    this.selectedTipoId = parseInt(event.target.value) || 0;
    console.log('Tipo seleccionada:', this.selectedTipoId);
    this.diagnotico.tiSo_Id = this.selectedTipoId;
  }

  onRowSelectP(inci_Id: number, item: any): void {
    this.selectedRowIndexP = inci_Id; // Guarda el usua_Id seleccionado
    console.log(`Usuario seleccionado con inci_Id: ${inci_Id}`);
    this.diagnotico.inci_Id = this.selectedRowIndexP; // Muestra los datos del usuario seleccionado el usua_Id a InsertAsignacion sin modificarlo
  }

  cargarFechaHora() {
    const now = new Date();
    // Reducimos las 5 horas para ajustar a la zona horaria local
    const adjustedDate = new Date(now.getTime() - (5 * 60 * 60 * 1000));

    // Formateamos la fecha en formato dd/mm/yyyy hh:mm
    const day = adjustedDate.getDate().toString().padStart(2, '0'); // Agrega un cero si es necesario
    const month = (adjustedDate.getMonth() + 1).toString().padStart(2, '0'); // Los meses son indexados desde 0, por lo que sumamos 1
    const year = adjustedDate.getFullYear();
    const hours = adjustedDate.getHours().toString().padStart(2, '0');
    const minutes = adjustedDate.getMinutes().toString().padStart(2, '0');

    // Formateamos la cadena como dd/mm/yyyy hh:mm
    this.fechaHoraString = `${day}/${month}/${year} ${hours}:${minutes}`;

    console.log("Fecha actual registro actualizada");
  }


  onFechaHoraChange(event: any) {
    const fechaHoraString = event.target.value;
  }

  onSubmit() {
    if (this.diagnotico.diag_Solucionado === null) {
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione si la incidencia fue solucionada o no.';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    if (this.diagnotico.diag_Solucionado && this.diagnotico.tiSo_Id === 0) {
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione un tipo de solución.';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    console.log("Id_Incidencia", this.diagnotico.inci_Id);
    this.diagnotico.idContratoUsuario = this.DatosUsuario[0].cont_Id;
    console.log("idcontrato", this.diagnotico.idContratoUsuario);
    console.log("Escalable", this.diagnotico.diag_Escalable);
    console.log("Datos a insertar", this.diagnotico);

    this.casodelegado.insertDiagnostico(this.diagnotico).subscribe({
      next: (response) => {
        console.log('Diagnóstico insertado con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Diagnóstico insertado con éxito';
        setTimeout(() => {
          window.location.reload();
          this.showNotification = false;
        }, 5000);
      },
      error: (error) => {
        console.error('Error al insertar el diagnóstico:', error);
        this.showNotification = true;
        this.notificationMessage = 'Error al insertar el diagnóstico';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000);
      }
    });
  }

}
