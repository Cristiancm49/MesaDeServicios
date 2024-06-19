import { Component } from '@angular/core';

@Component({
  selector: 'app-caso-excepcional',
  standalone: true,
  imports: [],
  templateUrl: './caso-excepcional.component.html',
  styleUrl: './caso-excepcional.component.css'
})
export class CasoExcepcionalComponent {
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
