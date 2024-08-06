import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { CasoRegistroService } from '../core/services/caso-registro.service';

interface AreaTec {
  id_AreaTec: number;
  nom_AreaTec: string;
  val_AreaTec: number;
}

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  templateUrl: './caso-registro.component.html',
  styleUrls: ['./caso-registro.component.css']
})
export class CasoRegistroComponent implements OnInit {
  areasTec: AreaTec[] = [];

  constructor(private casoRegistroService: CasoRegistroService) { }

  ngOnInit() {
    this.loadAreasTec();
  }

  loadAreasTec() {
    this.casoRegistroService.getAreasTec(1).subscribe({
      next: (data) => {
        this.areasTec = data;
        console.log('Areas Tec recibidas:', this.areasTec);
      },
      error: (error) => {
        console.error('Error fetching areas tec:', error);
      }
    });
  }
}
