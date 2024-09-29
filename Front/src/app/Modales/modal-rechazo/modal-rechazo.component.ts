import { Component, NgModule, OnInit, Inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CasoGestion } from '../../core/services/caso-gestion.service';
import { RechazarIncidencia } from '../../interfaces/CasoGestión/RechazarIncidencia';
import { MatDialogActions } from '@angular/material/dialog';

@Component({
  selector: 'app-modal-rechazo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal-rechazo.component.html',
  styleUrl: './modal-rechazo.component.css'
})
export class ModalRechazoComponent {

  rechazo: RechazarIncidencia[] = [];
  showNotification = false;
  notificationMessage = '';
  isLoading = true;


  RechazarIncidencia: RechazarIncidencia={

    inci_Id: 0,
    inTr_FechaActualizacion: new Date(),
    inTr_MotivoRechazo: ''

  }

  valorRecibido: any;
  constructor(
    public matDialogRef: MatDialogRef<ModalRechazoComponent>, private casoGestion: CasoGestion,
    @Inject(MAT_DIALOG_DATA) public data: { valor: any }
  ) {
    this.valorRecibido = data.valor;
    console.log('dato recibido',this.valorRecibido);
    this.RechazarIncidencia.inci_Id = this.valorRecibido;
  }


  ngOnInit() {
    this.cargarFechaHora();
  }

  onRechazar() {
    if (this.RechazarIncidencia.inci_Id === 0) {
      console.error('No se ha seleccionado ninguna incidencia');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, seleccione una incidencia antes de rechazar';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    if (!this.RechazarIncidencia.inTr_MotivoRechazo) {
      console.log("motivo de rechazo:", this.RechazarIncidencia.inTr_MotivoRechazo)
      console.error('No se ha proporcionado un motivo de rechazo');
      this.showNotification = true;
      this.notificationMessage = 'Por favor, proporcione un motivo de rechazo';
      setTimeout(() => {
        this.showNotification = false;
      }, 5000);
      return;
    }

    console.log("Datos de rechazo a enviar:", this.RechazarIncidencia);
    
    this.casoGestion.rechazarinciden(this.RechazarIncidencia).subscribe({
      next: (response) => {
        console.log('Incidencia rechazada con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Incidencia rechazada con éxito';
        setTimeout(() => {
          this.showNotification = false;
          window.location.reload();
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al rechazar la incidencia:', error);
        this.showNotification = true;
        this.notificationMessage = `Incidencia rechazada con éxito`;
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

  cargarFechaHora() {
    const now = new Date();
    this.RechazarIncidencia.inTr_FechaActualizacion = now;
    console.log("Fecha de rechazo", this.RechazarIncidencia.inTr_FechaActualizacion)
  }

  onMotivoRechazoChange(value: string) {
    console.log('Motivo de rechazo actualizado:', value);
    this.RechazarIncidencia.inTr_MotivoRechazo = value;
  }

}
