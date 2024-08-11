import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { AreaTec } from '../interfaces/area-tec';
import { DatosUser } from '../interfaces/DatosUser';
import { Categorias } from '../interfaces/Interfaz-categoria';
import { Incidencia } from '../interfaces/Insert-Incidencia';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink],
  templateUrl: './caso-registro.component.html',
  styleUrls: ['./caso-registro.component.css']
})
export class CasoRegistroComponent implements OnInit {
  areasTec: AreaTec[] = [];
  DatosUsuario: DatosUser[] = [];
  catego: Categorias[] = [];
  isLoading = true;
  selectedCategoriaId = 0;
  fechaHoraString: string = '';

  incidencia: Incidencia = {
    id_Incidencias: 20,
    idSolicitante_Incidencias: 0,
    esExc_Incidencias: false,
    idAdmin_IncidenciasExc: null,
    fechaHora_Incidencias: new Date(),
    id_AreaTec: 0,
    descrip_Incidencias: '',
    eviden_Incidencias: null,
    valTotal_Incidencias: 0
  };

  constructor(private casoRegistroService: CasoRegistroService) { }

  ngOnInit() {
    this.loadAreasTec(0);
    this.loadDatosUser();
    this.loadCategorias();
    this.cargarFechaHora();
  }

  loadAreasTec(selectedCategoriaId : number) {
    this.isLoading = true;
    this.casoRegistroService.getAreasTec(selectedCategoriaId).subscribe({
      next: (data) => {
        this.areasTec = data;
        this.isLoading = false; 
        console.log('Areas Tec:', this.areasTec);
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
    this.casoRegistroService.getDatosUsuario(1004446325).subscribe({
      next: (data) => {
        this.DatosUsuario = data;
        this.isLoading = false;
        console.log('Datos Usuario:', this.DatosUsuario);
      },
      error: (error) => {
        console.error('Error fetching Datos User:', error);
        this.isLoading = false;
      }
    });
  }

  loadCategorias() {
    this.isLoading = true;
    this.casoRegistroService.getCategorias(1).subscribe({
      next: (data) => {
        this.catego = data;
        this.isLoading = false;
        console.log('Good Categorias:', this.areasTec);
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
    this.incidencia.fechaHora_Incidencias = now;
    this.fechaHoraString = now.toISOString().slice(0, 16);
  }
  
  onFechaHoraChange(event: any) {
    const fechaHoraString = event.target.value;
    this.incidencia.fechaHora_Incidencias = new Date(fechaHoraString);
  }

  onAreaTecnicaSelected(event: any) {
    const selectedValue = event.target.value;
    this.incidencia.id_AreaTec = selectedValue ? parseInt(selectedValue, 10) : 0;
    console.log('Área técnica seleccionada:', this.incidencia.id_AreaTec);
  }

  onSubmit() {
    if (this.DatosUsuario.length > 0) {
      this.incidencia.idSolicitante_Incidencias = this.DatosUsuario[0].id_ChaLog;
    }

    console.log('id_Incidencias:', this.incidencia.id_Incidencias);
    
    if (!(this.incidencia.fechaHora_Incidencias instanceof Date) || isNaN(this.incidencia.fechaHora_Incidencias.getTime())) {
      this.incidencia.fechaHora_Incidencias = new Date();
    }


    console.log('Valores capturados:');
    console.log('id_Incidencias:', this.incidencia.id_Incidencias);
    console.log('idSolicitante_Incidencias:', this.incidencia.idSolicitante_Incidencias);
    console.log('esExc_Incidencias:', this.incidencia.esExc_Incidencias);
    console.log('idAdmin_IncidenciasExc:', this.incidencia.idAdmin_IncidenciasExc);
    console.log('fechaHora_Incidencias:', this.incidencia.fechaHora_Incidencias);
    console.log('id_AreaTec:', this.incidencia.id_AreaTec);
    console.log('descrip_Incidencias:', this.incidencia.descrip_Incidencias);
    console.log('eviden_Incidencias:', this.incidencia.eviden_Incidencias);
    console.log('valTotal_Incidencias:', this.incidencia.valTotal_Incidencias);
    
    
    this.casoRegistroService.insertIncidencia(this.incidencia).subscribe({
      next: (response) => {
        console.log('Incidencia insertada con éxito:', response);
        // Aquí puedes agregar lógica adicional después de la inserción exitosa
      },
      error: (error) => {
        console.error('Error al insertar la incidencia:', error);
      }
    });
  }
}