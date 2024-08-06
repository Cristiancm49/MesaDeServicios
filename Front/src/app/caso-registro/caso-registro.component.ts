import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { AreaTec } from '../interfaces/area-tec';
import { DatosUser } from '../interfaces/DatosUser';

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  templateUrl: './caso-registro.component.html',
  styleUrls: ['./caso-registro.component.css']
})
export class CasoRegistroComponent implements OnInit {
  areasTec: AreaTec[] = [];
  DatosUsuario: DatosUser[] = [];
  isLoading = true;

  constructor(private casoRegistroService: CasoRegistroService) { }

  ngOnInit() {
    this.loadAreasTec();
    this.loadDatosUser();
  }

  loadAreasTec() {
    this.isLoading = true;
    this.casoRegistroService.getAreasTec(1).subscribe({
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
}