import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-reservas-salas',
  standalone: true,
  imports: [],
  templateUrl: './reservas-salas.component.html',
  styleUrl: './reservas-salas.component.css'
})
export class ReservasSalasComponent {
  urlSegura: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {
    this.urlSegura = this.sanitizer.bypassSecurityTrustResourceUrl(
      'https://chaira.uniamazonia.edu.co/Reservas/views/login.aspx'
    );
  }
}
