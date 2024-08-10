import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { AreaTec } from '../interfaces/area-tec';
import { DatosUser } from '../interfaces/DatosUser';
import { Categorias } from '../interfaces/Interfaz-categoria';

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  templateUrl: './caso-registro.component.html',
  styleUrls: ['./caso-registro.component.css']
})
export class CasoRegistroComponent implements OnInit {
  areasTec: AreaTec[] = [];
  DatosUsuario: DatosUser[] = [];
  catego: Categorias[] = [];
  isLoading = true;
  selectedCategoriaId = 0;
  fechaHora: string = '';

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
    this.fechaHora = now.toISOString().slice(0, 16);
  }
}