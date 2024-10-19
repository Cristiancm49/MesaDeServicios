import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { AreaTec } from '../interfaces/CasoRegistro/area-tec';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { Categorias } from '../interfaces/CasoRegistro/Interfaz-categoria';
import { Incidencia } from '../interfaces/CasoRegistro/Insert-Incidencia';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Documento } from '../DatosLogin/User';

@Component({
  selector: 'app-caso-excepcional',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink],
  templateUrl: './caso-excepcional.component.html',
  styleUrl: './caso-excepcional.component.css'
})
export class CasoExcepcionalComponent implements OnInit{

  areasTec: AreaTec[] = [];
  DatosUsuario: DatosUser[] = [];
  DatosAdministrador: DatosUser[] = [];
  catego: Categorias[] = [];
  isLoading = true;
  selectedCategoriaId = 0;
  fechaHoraString: string = '';
  identificacionUsuario: string = '';
  valorprioridad: number = 0;
  notificationMessage = '';
  showNotification = false;
  intervalo: any;


  incidencia: Incidencia = {
    documentoSolicitante: 0,
    documentoAdmin: null,
    areaTecnica: 0,
    descripcion: ""
  };


  constructor(private casoRegistroService: CasoRegistroService) { 
  this.DatosUsuario = [];

  }


  ngOnInit() {
    this.loadAreasTec(0);
    this.loadDatosUser(0);
    this.loadCategorias();
    this.loadDatosAdmin();
    this.cargarFechaHora();

    this.intervalo = setInterval(() => {
      this.cargarFechaHora();
    }, 10000); 
  }

  ngOnDestroy(): void {
    if (this.intervalo) {
      clearInterval(this.intervalo);
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

  buscarUsuario() {
    if (this.identificacionUsuario) {
      const identificacion = parseInt(this.identificacionUsuario, 10);
      if (!isNaN(identificacion)) {
        this.loadDatosUser(identificacion);
      } else {
        console.error('Por favor, ingrese una identificación válida (solo números)');
      }
    } else {
      console.error('Por favor, ingrese una identificación');
    }
  }

  getNombreSolicitante(): string {
    return this.DatosUsuario[0]?.nombreCompleto || '';
  }
  
  getCargo(): string {
    return this.DatosUsuario[0]?.tnom_Descripcion|| '';
  }
  
  getDependencia(): string {
    return this.DatosUsuario[0]?.unid_Nombre || '';
  }

  loadDatosUser(identificacion: number) {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    this.casoRegistroService.getDatosUsuario(identificacion).subscribe({
      next: (response) => {
        this.DatosUsuario = response.data || [];  // Accede a 'data'
        this.isLoading = false;
        console.log('Datos Usuario:', this.DatosUsuario);
      },
      error: (error) => {
        console.error('Error fetching Datos User:', error);
        this.isLoading = false;
      }
    });
  }

  loadDatosAdmin() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    this.casoRegistroService.getDatosUsuario(Documento).subscribe({
      next: (response) => {
        this.DatosAdministrador = response.data || [];  // Accede a 'data'
        this.isLoading = false;
        console.log('Datos Usuario:', this.DatosAdministrador);
      },
      error: (error) => {
        console.error('Error fetching Datos User:', error);
        this.isLoading = false;
      }
    });
  }

  loadCategorias() {
    this.isLoading = true;
    this.casoRegistroService.getCategorias().subscribe({
      next: (response) => {
        this.catego = response.data || [];  // Accede a 'data'
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
  }

  cargarFechaHora() {
    const now = new Date();

    // Reducimos las 5 horas para ajustar a la zona horaria local
    const adjustedDate = new Date(now.getTime() - (5 * 60 * 60 * 1000));

    // Convertimos la fecha ajustada a ISO string sin la diferencia horaria
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
      this.incidencia.documentoSolicitante = parseInt(this.DatosUsuario[0].peGe_DocumentoIdentidad, 10);
    }

    console.log('id_Incidencias:', this.incidencia.documentoSolicitante);
    console.log('Valores capturados:');
    console.log('IdSolicitante:', this.incidencia.documentoSolicitante);
    console.log('IdAdmin:', this.incidencia.documentoAdmin);
    console.log('areatecnica:', this.incidencia.areaTecnica);
    console.log('descripcion:', this.incidencia.descripcion);

    if (this.DatosAdministrador.length > 0) {
      this.incidencia.documentoAdmin = parseInt(this.DatosAdministrador[0].peGe_DocumentoIdentidad, 10);
    }

    
    this.casoRegistroService.insertIncidencia(this.incidencia).subscribe({
      next: (response) => {
        console.log('Incidencia insertada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia Excepcional insertada con éxito';
        this.resetIncidencia();
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

  private resetIncidencia() {
    this.incidencia = {
      documentoSolicitante: 0,
      documentoAdmin: 0,
      areaTecnica: 0,
      descripcion: ''
    };
    this.cargarFechaHora();
    this.selectedCategoriaId = 0;
    this.loadAreasTec(0);
    this.DatosUsuario = [];
    this.identificacionUsuario = '';
  }
}
