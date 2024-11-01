import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CasoRegistroService } from '../core/services/caso-registro.service';
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { insertrol } from '../interfaces/Rol/insertrol';
import { CasoGestion } from '../core/services/caso-gestion.service';
import { Rol } from '../core/services/Rol.service';
import {ViewRoles} from '../interfaces/CasoGestión/ViewRoles'

@Component({
  selector: 'app-administrar-rol',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink],
  templateUrl: './administrar-rol.component.html',
  styleUrl: './administrar-rol.component.css'
})
export class AdministrarRolComponent implements OnInit{

  identificacionUsuario: string = '';
  DatosUsuario: DatosUser[] = [];
  vistaroles: ViewRoles[] = [];
  isLoading = true;
  selectedRowIndex: number | null = null;
  isRowSelected: boolean = false;
  selectedRowIndexP: number | null = null;
  selectedRolId = 0;
  Pasarid = 0;
  notificationMessage = '';
  showNotification = false;

  Rol: insertrol = {
    cont_Id: 0,
    usRo_Id: 0
  };

  constructor(private casoRegistroService: CasoRegistroService, private casoGestion: CasoGestion, private roles: Rol) { 
    this.DatosUsuario = [];
  }

  ngOnInit() {
    this.loadDatosRol()
      this.loadDatosUser(0);
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

  loadDatosUser(identificacion: number) {
    this.isLoading = true;
    console.log('Requesting DatosUsuario...');
    this.casoRegistroService.getDatosUsuario(identificacion).subscribe({
      next: (response) => {
        this.DatosUsuario = response.data || [];
        this.isLoading = false;
        console.log('Datos Usuario:', this.DatosUsuario);
      },
      error: (error) => {
        console.error('Error fetching Datos User:', error);
        this.isLoading = false;
      }
    });
  }

  onRowSelect(index: number, inci_Id: number): void {
    this.selectedRowIndex = index;
    this.isRowSelected = true;
    console.log(`Fila seleccionada: ${index}`);
    console.log(`Id contrato persona: ${inci_Id}`);
    this.Pasarid = inci_Id;
  }

  loadDatosRol() {
    this.isLoading = true;
    console.log('Requesting Datos Rol...');
    
    this.casoGestion.getRoles().subscribe({
      next: (response) => {
        this.vistaroles = response.data || [];
        this.isLoading = false;
        console.log('Datos Rol:', this.vistaroles);
      },
      error: (error) => {
        console.error('Sin datos de Rol:', error);
        this.isLoading = false;
      }
    });
  }

  onRolSelected(event: any) {
    this.selectedRolId = parseInt(event.target.value) || 0;
    console.log('Rol seleccionado:', this.selectedRolId);

  }

  onSubmit() {
    if (this.DatosUsuario.length > 0) {
      this.Rol.cont_Id = this.DatosUsuario[0].cont_Id;
    }

    this.Rol.usRo_Id = this.selectedRolId;

    console.log('Valores capturados:');
    console.log('IdContrato:', this.Rol.cont_Id);
    console.log('IdRol:', this.Rol.usRo_Id);

    this.roles.insertrol(this.Rol).subscribe({
      next: (response) => {
        console.log('Rol insertado con éxito:', response);
        this.showNotification = true;
        this.notificationMessage = 'Rol insertado con éxito';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000); 
      },
      error: (error) => {
        console.error('Error al insertar el rol:', error);
        this.showNotification = true;
        this.notificationMessage = 'Error al insertar el rol';
        setTimeout(() => {
          this.showNotification = false;
        }, 5000);
      }
    });
  }

}
