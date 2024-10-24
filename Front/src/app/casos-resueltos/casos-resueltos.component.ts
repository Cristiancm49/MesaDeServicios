import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserDataStateService } from '../core/Datos/datos-usuario.service'; 
import { DatosUser } from '../interfaces/CasoRegistro/DatosUser';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Documento } from '../DatosLogin/User';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-casos-resueltos',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterOutlet, RouterLink],
  templateUrl: './casos-resueltos.component.html',
  styleUrl: './casos-resueltos.component.css'
})
export class CasosResueltosComponent implements OnInit, OnDestroy {
  DatosUsuario: DatosUser[] = [];
  isLoading = true;
  showNotification = false;
  notificationMessage = '';
  private userDataSubscription: Subscription | undefined;


  constructor(
    private userDataState: UserDataStateService
  ) { }
  

  ngOnInit(): void {
    this.setupUserData(); // Nuevo método para configurar los datos del usuario
  }

  ngOnDestroy(): void {

    // Cancelamos la suscripción al destruir el componente
    if (this.userDataSubscription) {
      this.userDataSubscription.unsubscribe();
    }
  }

  private setupUserData(): void {
    // Nos suscribimos al loading state
    this.userDataState.loading$.subscribe(
      isLoading => this.isLoading = isLoading
    );

    // Nos suscribimos a los datos del usuario
    this.userDataSubscription = this.userDataState.userData$.subscribe({
      next: (data) => {
        if (data) {
          this.DatosUsuario = data;
          console.log('Datos Usuario:', this.DatosUsuario);
        }
      },
      error: (error) => {
        console.error('Error en la suscripción de datos de usuario:', error);
      }
    });

    // Cargamos los datos solo si no están ya cargados
    if (!this.userDataState.currentUserData) {
      this.userDataState.loadUserData(Documento);
    }
  }

}
