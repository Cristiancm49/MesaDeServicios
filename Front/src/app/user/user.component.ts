import { Component } from '@angular/core';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  constructor() {}

  onSubmit() {
    // Lógica para enviar el formulario
    alert('Formulario enviado');
  }

  onReset() {
    // Lógica para resetear el formulario
    alert('Formulario reseteado');
  }
}
