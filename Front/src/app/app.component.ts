import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CasoRegistroComponent } from './caso-registro/caso-registro.component';
import { CasoExcepcionalComponent } from "./caso-excepcional/caso-excepcional.component";
import { CasoActivoComponent } from './caso-activo/caso-activo.component';
import { CasosResueltosComponent } from './casos-resueltos/casos-resueltos.component';
import { CasosDelegadosComponent } from './casos-delegados/casos-delegados.component';
import { InicioComponent } from './inicio/inicio.component';
import { CasoFinalizadoComponent } from './caso-finalizado/caso-finalizado.component';
import { CasosHistorialComponent } from './casos-historial/casos-historial.component';
import { AdministrarRolComponent } from './administrar-rol/administrar-rol.component';
import { CasosGestionComponent } from './casos-gestion/casos-gestion.component';
import { CasosSeguimientoComponent } from './casos-seguimiento/casos-seguimiento.component';
import { UserComponent } from "./user/user.component";
import { MenuComponent } from './menu/menu.component';



@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [RouterOutlet, CasoRegistroComponent, CasoExcepcionalComponent, CasoActivoComponent, CasosResueltosComponent, CasosDelegadosComponent, InicioComponent, CasoFinalizadoComponent, CasosHistorialComponent, AdministrarRolComponent, CasosGestionComponent, CasosSeguimientoComponent, UserComponent, MenuComponent]
})


export class AppComponent {
  title = 'mesadeservicios';
}
