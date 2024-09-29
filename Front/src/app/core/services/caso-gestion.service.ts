import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidencia } from '../../interfaces/CasoGestión/ViewIndicencia';
import { ViewPersonalAsignacion } from '../../interfaces/CasoGestión/ViewPersonalAsignacion';
import { InsertAsignacion } from '../../interfaces/CasoGestión/Insert-Asignacion';
import { ViewRoles } from '../../interfaces/CasoGestión/ViewRoles';
import { RechazarIncidencia } from '../../interfaces/CasoGestión/RechazarIncidencia';
import { SelectPrioridad } from '../../interfaces/CasoGestión/SelectPrioridad';
import { CambioPrioridad } from '../../interfaces/CasoGestión/CambioPrioridad';


@Injectable({
  providedIn: 'root'
})
export class CasoGestion {
  private selectincidencia = 'https://localhost:44346/api/v5/Incidencia/SelectIncidenciasRegistradas';
  private selectpersonal = 'https://localhost:44346/api/v5/GestionIncidencia/UsuariosConIncidenciasAsignadas';
  private insertarasignacion = 'https://localhost:44346/api/v5/GestionIncidencia';
  private selectroles = 'https://localhost:44346/api/v2/Roles/SelectRolesUsuario';
  private rechazar = 'https://localhost:44346/api/v5/GestionIncidencia';
  private prioridad = 'https://localhost:44346/api/v5/GestionIncidencia/SelectPrioridad';
  private cambioprioridad = 'https://localhost:44346/api/v5/GestionIncidencia';

  constructor(private http: HttpClient) { }


  insertIncidencia(): Observable<ViewIncidencia[]> {
    return this.http.get<ViewIncidencia[]>(`${this.selectincidencia}`);
  }

  mostrarpersonal(id_Rol: number): Observable<ViewPersonalAsignacion[]> {
    return this.http.get<ViewPersonalAsignacion[]>(`${this.selectpersonal}?id_Rol=${id_Rol}`);
  }


  insertasignacion(asignacion: InsertAsignacion): Observable<HttpResponse<any>> {
    const url = `${this.insertarasignacion}/AsignarUsuario`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, asignacion, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }

  getRoles(): Observable<ViewRoles[]> {
    return this.http.get<ViewRoles[]>(`${this.selectroles}`);
  }


  rechazarinciden(rechazo: RechazarIncidencia): Observable<HttpResponse<any>> {
    const url = `${this.rechazar}/RechazarIncidencia`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, rechazo, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }

  getPrioridad(): Observable<SelectPrioridad[]> {
    return this.http.get<SelectPrioridad[]>(`${this.prioridad}`);
  }


  cambiarprioridad(nuevapri: CambioPrioridad): Observable<HttpResponse<any>> {
    const url = `${this.cambioprioridad}/CambioPrioridad`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, nuevapri, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }

}