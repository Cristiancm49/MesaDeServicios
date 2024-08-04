import { Component } from '@angular/core';

@Component({
  selector: 'app-caso-registro',
  standalone: true,
  imports: [],
  templateUrl: './caso-registro.component.html',
  styleUrl: './caso-registro.component.css'
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
