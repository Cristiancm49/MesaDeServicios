import { Component, NgModule, OnInit, Inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CasoGestion } from '../../core/services/caso-gestion.service';
import { SelectPrioridad } from '../../interfaces/CasoGestión/SelectPrioridad';
import { CambioPrioridad } from '../../interfaces/CasoGestión/CambioPrioridad';
import { MatDialogActions } from '@angular/material/dialog';


@Component({
  selector: 'app-modal-prioridad',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal-prioridad.component.html',
  styleUrl: './modal-prioridad.component.css'
})
export class ModalPrioridadComponent {

  vistraprioridad: SelectPrioridad[] = [];
  selectedPrioridadId = 0;
  cambioprioridad: CambioPrioridad[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;

  prioriadcambio: CambioPrioridad={
    inci_Id: 0,
    new_Prioridad: 0,
    motivoCambio: ''
  }

  valorRecibido: any;
  constructor(
    public matDialogRef: MatDialogRef<ModalPrioridadComponent>, private casoGestion: CasoGestion,
    @Inject(MAT_DIALOG_DATA) public data: { valor: any }
  ) {
    this.valorRecibido = data.valor;
    console.log('dato recibido',this.valorRecibido);
    this.prioriadcambio.inci_Id = this.valorRecibido;
  }

  ngOnInit() {
    this.loadPrioridades();
  }

  onPrioridad() {
    console.log('Estado actual de prioriadcambio al iniciar onPrioridad:', this.prioriadcambio);

    if (this.prioriadcambio.inci_Id === 0) {
      console.error('No se ha seleccionado ninguna incidencia');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione una incidencia antes de cambiar la prioridad';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    if (!this.prioriadcambio.motivoCambio || this.prioriadcambio.motivoCambio.trim() === '') {
      console.error('No se ha proporcionado un motivo de cambio');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, proporcione un motivo de cambio';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    console.log("Datos de prioridad a enviar:", this.prioriadcambio);
    
    this.casoGestion.cambiarprioridad(this.prioriadcambio).subscribe({
      next: (response) => {
        console.log('Prioridad cambiada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Prioridad cambiada con éxito';
        setTimeout(() => {
          this.showNotification = false;
          window.location.reload();
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al cambiar la prioridad:', error);
        this.showNotification = true;
        this.notificationMessage = 'Prioridad cambiada con éxito';
        setTimeout(() => {
          this.showNotification = false;
          window.location.reload();
        }, 5000);
      }
    });
  }

  onCancel(): void {
    // Cierra el modal sin hacer cambios
    this.matDialogRef.close() 
  }

  loadPrioridades() {
    this.isLoading = true;
    this.casoGestion.getPrioridad().subscribe({
      next: (data) => {
        this.vistraprioridad = data;
        this.isLoading = false;
        console.log('Good Prioridades:', this.vistraprioridad);
      },
      error: (error) => {
        console.error('Error al traer prioridades:', error);
        this.isLoading = false;
      }
    });
  }


  onPrioridadSelected(event: any) {
    this.selectedPrioridadId = parseInt(event.target.value) || 0;
    console.log('Prioridad seleccionada:', this.selectedPrioridadId);
    this.prioriadcambio.new_Prioridad = this.selectedPrioridadId;
  }


}
