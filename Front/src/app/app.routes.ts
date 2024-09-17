import { Routes } from '@angular/router';
import { CasoRegistroComponent } from './caso-registro/caso-registro.component';
import { CasosGestionComponent } from './casos-gestion/casos-gestion.component';
import { CasosSeguimientoComponent } from './casos-seguimiento/casos-seguimiento.component';
import { RedirectGuard } from './redirect.guard';
import { CasoExcepcionalComponent } from './caso-excepcional/caso-excepcional.component';
import { UserComponent } from './user/user.component';
import { InicioComponent } from './inicio/inicio.component';
import { CasoActivoComponent } from './caso-activo/caso-activo.component';
import { CasosResueltosComponent } from './casos-resueltos/casos-resueltos.component';
import { CasosHistorialComponent } from './casos-historial/casos-historial.component';
import { AdministrarRolComponent } from './administrar-rol/administrar-rol.component';
import { CasoFinalizadoComponent } from './caso-finalizado/caso-finalizado.component';
import { CasosDelegadosComponent } from './casos-delegados/casos-delegados.component';
import { DisponibilidadSalasComponent } from './disponibilidad-salas/disponibilidad-salas.component';
import { ReservasSalasComponent } from './reservas-salas/reservas-salas.component';
export const routes: Routes = [
  {
    path: '',
    component: InicioComponent,
    pathMatch: 'full'
  },
  {
    path: 'gestion-casos',
    component: CasosGestionComponent
  },
  {
    path: 'DisponibilidadSalas',
    component: DisponibilidadSalasComponent
  },
  {
    path: 'ReservaSalas',
    component: ReservasSalasComponent
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
  },

  {
    path: 'Inicio',
    component: InicioComponent
  },

  {
    path: 'Caso-activo',
    component: CasoActivoComponent
  },

  {
    path: 'Caso-resuelto',
    component: CasosResueltosComponent
  },
  {
    path: 'Caso-historial',
    component: CasosHistorialComponent
  },
  {
    path: 'Admi-Rol',
    component: AdministrarRolComponent
  },
  {
    path: 'Caso-Delegado',
    component: CasosDelegadosComponent
  },
  {
    path: 'Caso-Finalizado',
    component: CasoFinalizadoComponent
  }

];
