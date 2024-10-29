import { Component, NgModule, OnInit, Inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InsertEscaladoExterno } from '../../interfaces/CasoSeguimiento/InsertExterno';
import { Seguimiento } from '../../core/services/Seguimiento.service';

@Component({
  selector: 'app-modal-escalar-externo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal-escalar-externo.component.html',
  styleUrl: './modal-escalar-externo.component.css'
})
export class ModalEscalarExternoComponent {

  motivoEscalamiento: string = '';
  valorRecibido: any;

  externo: InsertEscaladoExterno = {
    
    inci_Id: 0,
    descripcion: ""
    
  }

  constructor(
    public matDialogRef: MatDialogRef<ModalEscalarExternoComponent>, private casoseguimiento: Seguimiento,
    @Inject(MAT_DIALOG_DATA) public data: { valor: any }
  ) {
    this.valorRecibido = data.valor;
    console.log('dato recibido',this.valorRecibido);
    this.externo.inci_Id = this.valorRecibido;
  }

  onSubmit() {
    
    this.externo.descripcion = this.motivoEscalamiento;
    console.log('Valores para delegar:');
    console.log('Id Incidencia:', this.externo.inci_Id);
    console.log('Id Personal:', this.externo.descripcion);
    
    this.casoseguimiento.insertEscaladoExterno(this.externo).subscribe({
      next: (response) => {
        console.log('Incidencia escalada externamente con éxito:', response);
        setTimeout(() => {
          window.location.reload();
        }, 1000); 
      },
      error: (error) => {
        console.error('Error al asignar la incidencia:', error);
        setTimeout(() => {
          window.location.reload();
        }, 1000);
      }
    });

    this.matDialogRef.close();
  }

  onAceptar(): void {
    this.onSubmit();
    this.matDialogRef.close(); // Retorna el motivo al cerrar el modal
  }

  onCancelar(): void {
    this.matDialogRef.close(); // Cierra el modal sin devolver datos
  }

}
