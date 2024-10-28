import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { UserDataStateService } from '../core/Datos/datos-usuario.service'; // Importamos el nuevo servicio
import { AreaTec } from '../interfaces/CasoRegistro/area-tec';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { Categorias } from '../interfaces/CasoRegistro/Interfaz-categoria';
import { Incidencia } from '../interfaces/CasoRegistro/Insert-Incidencia';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Documento } from '../DatosLogin/User';
import { Subscription } from 'rxjs'; // Importamos Subscription para manejar las suscripciones

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink],
  templateUrl: './caso-registro.component.html',
  styleUrls: ['./caso-registro.component.css']
})
export class CasoRegistroComponent implements OnInit, OnDestroy {
  areasTec: AreaTec[] = [];
  DatosUsuario: DatosUser[] = [];
  catego: Categorias[] = [];
  isLoading = true;
  selectedCategoriaId = 0;
  fechaHoraString: string = '';
  showNotification = false;
  notificationMessage = '';
  valorprioridad: number = 0;
  intervalo: any;
  private userDataSubscription: Subscription | undefined;

  incidencia: Incidencia = {
    idContratoSolicitante: 0,
    valorUnidadSolicitante: 0,
    idContratoAdmin: null,
    areaTecnica: 0,
    descripcion: ""
  };

  constructor(
    private casoRegistroService: CasoRegistroService,
    private userDataState: UserDataStateService // Inyectamos el nuevo servicio
  ) { }

  ngOnInit(): void {
    this.setupUserData(); // Nuevo método para configurar los datos del usuario
    this.loadCategorias();
    this.cargarFechaHora();

    this.intervalo = setInterval(() => {
      this.cargarFechaHora();
    }, 10000);
  }

  ngOnDestroy(): void {
    if (this.intervalo) {
      clearInterval(this.intervalo);
    }
    // Cancelamos la suscripción al destruir el componente
    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  // Nuevo método para configurar los datos del usuario
  private setupUserData(): void {
    // Nos suscribimos al loading state
    this.userDataState.loading$.subscribe(
      isLoading => this.isLoading = isLoading
    );

    // Nos suscribimos a los datos del usuario
    this.userDataSubscription = this.userDataState.userData$.subscribe({
      next: (data) => {
        if (data) {
          this.DatosUsuario = data;
          console.log('Datos Usuario:', this.DatosUsuario);
        }
      },
      error: (error) => {
        console.error('Error en la suscripción de datos de usuario:', error);
      }
    });

    // Cargamos los datos solo si no están ya cargados
    if (!this.userDataState.currentUserData) {
      this.userDataState.loadUserData(Documento);
    }
  }

  loadAreasTec(selectedCategoriaId: number) {
    this.isLoading = true;
    this.casoRegistroService.getAreasTec(selectedCategoriaId).subscribe({
      next: (response) => {
        this.areasTec = response.data || [];  
        this.isLoading = false;
        console.log('Areas Tec:', this.areasTec);
      },
      error: (error) => {
        console.error('Error fetching areas tec:', error);
        this.isLoading = false;
      }
    });
  }

  loadCategorias() {
    this.isLoading = true;
    this.casoRegistroService.getCategorias().subscribe({
      next: (response) => {
        this.catego = response.data || [];  
        this.isLoading = false;
        console.log('Good Categorias:', this.catego);
      },
      error: (error) => {
        console.error('Error fetching categorias:', error);
        this.isLoading = false;
      }
    });
  }

  onCategoriaSelected(event: any) {
    this.selectedCategoriaId = parseInt(event.target.value) || 0;
    console.log('Categoría seleccionada:', this.selectedCategoriaId);
    this.loadAreasTec(this.selectedCategoriaId);
    this.incidencia.areaTecnica = 0;
    this.areasTec = [];
    console.log('dato restablecido', this.incidencia.areaTecnica);
    this.loadAreasTec(this.selectedCategoriaId);
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      console.log('Archivo seleccionado:', file.name);
    }
  }

  cargarFechaHora() {
    const now = new Date();
    const adjustedDate = new Date(now.getTime() - (5 * 60 * 60 * 1000));
    this.fechaHoraString = adjustedDate.toISOString().slice(0, 16);
    console.log("Fecha actual registro actualizada");
  }

  onFechaHoraChange(event: any) {
    const fechaHoraString = event.target.value;
  }

  onAreaTecnicaSelected(event: any) {
    const selectedValue = event.target.value;
    const lastValue = selectedValue.split(':').pop();
    this.incidencia.areaTecnica = lastValue ? parseInt(lastValue, 10) : 0;
    console.log('Área técnica seleccionada:', this.incidencia.areaTecnica);
  }

  onSubmit() {
    if (this.DatosUsuario.length > 0) {
      this.incidencia.idContratoSolicitante = this.DatosUsuario[0].cont_Id;
      this.incidencia.valorUnidadSolicitante = parseInt(this.DatosUsuario[0].unid_Nivel, 10);
    }
    
    console.log('Valores capturados:', this.incidencia);
    console.log("Prueba", this.DatosUsuario[0].peGe_Id);
    
    this.casoRegistroService.insertIncidencia(this.incidencia).subscribe({
      next: (response) => {
        console.log('Incidencia insertada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia insertada con éxito';
        this.Reset();
        setTimeout(() => {
          this.showNotification = false;
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al insertar la incidencia:', error);
        this.showNotification = true;
        this.notificationMessage = 'Error al insertar la incidencia';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000);
      }
    });
  }

  Reset() {
    this.incidencia.descripcion = '';
    this.selectedCategoriaId = 0;
    this.loadAreasTec(0);
    this.loadCategorias();
  }
}