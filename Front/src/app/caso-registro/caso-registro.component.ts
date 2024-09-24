import { Component, OnInit, OnDestroy } from '@angular/core';
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

  incidencia: Incidencia = {
    cont_IdSolicitante: 0,
    usua_IdAdminExc: null,
    inci_FechaRegistro: new Date(),
    arTe_Id: 0,
    inci_Descripcion: '',
    inci_ValorTotal: 0
  };

  constructor(private casoRegistroService: CasoRegistroService) { }

  ngOnInit(): void {
    this.loadAreasTec(0);
    this.loadDatosUser();
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
  }

  loadAreasTec(selectedCategoriaId: number) {
    this.isLoading = true;
    this.casoRegistroService.getAreasTec(selectedCategoriaId).subscribe({
      next: (data) => {
        this.areasTec = data;
        this.isLoading = false;
        console.log('Areas Tec:', this.areasTec);
        this.calcularPrioridad();
      },
      error: (error) => {
        console.error('Error fetching areas tec:', error);
        this.isLoading = false;
      }
    });
  }

  loadDatosUser() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    this.casoRegistroService.getDatosUsuario(Documento).subscribe({
      next: (data) => {
        this.DatosUsuario = data;
        this.isLoading = false;
        console.log('Datos Usuario:', this.DatosUsuario);
        this.calcularPrioridad();
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
      next: (data) => {
        this.catego = data;
        this.isLoading = false;
        console.log('Good Categorias:', this.catego);
        this.calcularPrioridad();
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
    this.incidencia.arTe_Id = 0;
    this.areasTec = [];
    console.log('dato restablecido', this.incidencia.arTe_Id);
    this.loadAreasTec(this.selectedCategoriaId);
  }

  cargarFechaHora() {
    const now = new Date();
    this.incidencia.inci_FechaRegistro = now;

    // Reducimos las 5 horas para ajustar a la zona horaria local
    const adjustedDate = new Date(now.getTime() - (5 * 60 * 60 * 1000));

    // Convertimos la fecha ajustada a ISO string sin la diferencia horaria
    this.fechaHoraString = adjustedDate.toISOString().slice(0, 16);

    console.log("Fecha actual registro actualizada");
}

  calcularPrioridad(): void {
    if (this.DatosUsuario.length > 0 && this.catego.length > 0 && this.areasTec.length > 0) {
      const valorUsuario = this.DatosUsuario[0].unid_Valor || 0;
      const valorCategoria = this.catego.find(c => c.caAr_Id === this.selectedCategoriaId)?.caAr_Valor || 0;
      const valorAreaTec = this.areasTec.find(a => a.arTe_Id === this.incidencia.arTe_Id)?.arTe_Valor || 0;
      
      this.valorprioridad = valorUsuario + valorCategoria + valorAreaTec;
      this.incidencia.inci_ValorTotal = this.valorprioridad;
      console.log('Prioridad calculada:', this.valorprioridad);
    } else {
      console.log('No se puede calcular la prioridad aún, faltan datos');
    }
  }
  
  onFechaHoraChange(event: any) {
    const fechaHoraString = event.target.value;
    this.incidencia.inci_FechaRegistro = new Date(fechaHoraString);
  }

  onAreaTecnicaSelected(event: any) {
    const selectedValue = event.target.value;
    const lastValue = selectedValue.split(':').pop();
    this.incidencia.arTe_Id = lastValue ? parseInt(lastValue, 10) : 0;
    console.log('Área técnica seleccionada:', this.incidencia.arTe_Id);
    this.calcularPrioridad();
  }

  onSubmit() {
    if (this.DatosUsuario.length > 0) {
      this.incidencia.cont_IdSolicitante = this.DatosUsuario[0].cont_Id;
    }

    console.log('id_Incidencias:', this.incidencia.cont_IdSolicitante);
    
    if (!(this.incidencia.inci_FechaRegistro instanceof Date) || isNaN(this.incidencia.inci_FechaRegistro.getTime())) {
      this.incidencia.inci_FechaRegistro = new Date();
    }


    console.log('Valores capturados:');
    console.log('cont_IdSolicitante:', this.incidencia.cont_IdSolicitante);
    console.log('usua_IdAdminExc:', this.incidencia.usua_IdAdminExc);
    console.log('inci_FechaRegistro:', this.incidencia.inci_FechaRegistro);
    console.log('arTe_Id:', this.incidencia.arTe_Id);
    console.log('inci_Descripcion:', this.incidencia.inci_Descripcion);
    console.log('inci_ValorTotal:', this.incidencia.inci_ValorTotal);
    
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
    this.incidencia.inci_Descripcion = '';
    this.selectedCategoriaId = 0;
    this.loadAreasTec(0);
    this.loadCategorias();
  }
  
}