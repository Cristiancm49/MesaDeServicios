import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-disponibilidad-salas',
  standalone: true,
  imports: [],
  templateUrl: './disponibilidad-salas.component.html',
  styleUrl: './disponibilidad-salas.component.css'
})
export class DisponibilidadSalasComponent {
  urlSegura: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {
    this.urlSegura = this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://chaira.uniamazonia.edu.co/Reservas/Views/Public/Salas.aspx?tipo=Sistemas'
    );
  }
}
