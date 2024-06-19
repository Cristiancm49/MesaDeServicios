import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserComponent } from './user/user.component';
import { CasoExcepcionalComponent } from "./caso-excepcional/caso-excepcional.component";
import { CasoActivoComponent } from './caso-activo/caso-activo.component';
import { CasosResueltosComponent } from './casos-resueltos/casos-resueltos.component';
import { CasosDelegadosComponent } from './casos-delegados/casos-delegados.component';



@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    imports: [RouterOutlet, UserComponent, CasoExcepcionalComponent, CasoActivoComponent, CasosResueltosComponent, CasosDelegadosComponent]
})


export class AppComponent {
  title = 'mesadeservicios';
}
