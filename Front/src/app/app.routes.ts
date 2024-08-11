import { Routes } from '@angular/router';
import { CasoRegistroComponent } from './caso-registro/caso-registro.component';
import { CasosGestionComponent } from './casos-gestion/casos-gestion.component';
import { CasosSeguimientoComponent } from './casos-seguimiento/casos-seguimiento.component';
import { RedirectGuard } from './redirect.guard';
import { CasoExcepcionalComponent } from './caso-excepcional/caso-excepcional.component';
import { UserComponent } from './user/user.component';
export const routes: Routes = [
  {
    path: '',
    component: CasoRegistroComponent,
    pathMatch: 'full'
  },
  {
    path: 'gestion-casos',
    component: CasosGestionComponent
  },
  {
    path: 'DisponibilidadSalas',
    canActivate: [RedirectGuard],
    component: RedirectGuard,
    data: {
      externalUrl: 'https://chaira.uniamazonia.edu.co/Reservas/Views/Public/Salas.aspx?tipo=Sistemas'
    }
  },
  {
    path: 'seguimiento-casos',
    component: CasosSeguimientoComponent
  },

  {
    path: 'caso-excepcional',
    component: CasoExcepcionalComponent 
  },

  {
    path: 'registar-caso',
    component: CasoRegistroComponent
  }

];
