import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Casodelegado} from '../core/services/caso-delegados.service';
import { ViewIncidenciaAsignada } from '../interfaces/ViewIncidenciaAsignada';

@Component({
  selector: 'app-casos-delegados',
  standalone: true,
  imports: [CommonModule, FormsModule ],
  templateUrl: './casos-delegados.component.html',
  styleUrl: './casos-delegados.component.css'
})
export class CasosDelegadosComponent {

  vistaasignada : ViewIncidenciaAsignada[] = []; 
  showNotification = false;
  notificationMessage = '';
  isLoading = true;
  loginusuario = 1004446325
  selectedRowIndexP: number | null = null;

  constructor(private casoActivo: Casodelegado) { }

  ngOnInit() {
    this.loadDatosIncidencia();
  }


  loadDatosIncidencia() {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    
    this.casoActivo.selectIncidenciaasignada(this.loginusuario).subscribe({
      next: (data) => {
        this.vistaasignada = data;
        this.isLoading = false;
        console.log('Datos Incidencia asignada:', this.vistaasignada);
      },
      error: (error) => {
        console.error('Sin datos de Incidencia asignada:', error);
        this.isLoading = false;
      }
    });
  }

  onRowSelectP(usua_Id: number, item: any): void {
    this.selectedRowIndexP = usua_Id; // Guarda el usua_Id seleccionado
    console.log(`Usuario seleccionado con usua_Id: ${usua_Id}`);
    console.log('Datos del usuario seleccionado:', item); // Muestra los datos del usuario seleccionado el usua_Id a InsertAsignacion sin modificarlo
    }

}
