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
import { ApiResponse } from '../../interfaces/Api/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class CasoGestion {
  private selectincidencia = 'https://localhost:44346/api/Incidencia/Gestion/consultar-IncidenciasResgistradas';
  private selectpersonal = 'https://localhost:44346/api/Incidencia/Gestion/consultar-usuario';
  private insertarasignacion = 'https://localhost:44346/api/Incidencia/Gestion';
  private selectroles = 'https://localhost:44346/api/Incidencia/Gestion/consultar-rolesUsuarios';
  private rechazar = 'https://localhost:44346/api/Incidencia/Gestion';
  private prioridad = 'https://localhost:44346/api/v5/GestionIncidencia/SelectPrioridad';
  private cambioprioridad = 'https://localhost:44346/api/v5/GestionIncidencia';

  constructor(private http: HttpClient) { }


  insertIncidencia(): Observable<ApiResponse<ViewIncidencia>> {
    return this.http.get<ApiResponse<ViewIncidencia>>(`${this.selectincidencia}`);
  }

  mostrarpersonal(id_Rol: number): Observable<ApiResponse<ViewPersonalAsignacion>> {
    return this.http.get<ApiResponse<ViewPersonalAsignacion>>(`${this.selectpersonal}?usRoId=${id_Rol}`);
  }


  insertasignacion(asignacion: InsertAsignacion): Observable<HttpResponse<any>> {
    const url = `${this.insertarasignacion}/asignar-Incidencia`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, asignacion, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }

  getRoles(): Observable<ApiResponse<ViewRoles>> {
    return this.http.get<ApiResponse<ViewRoles>>(`${this.selectroles}`);
  }


  rechazarinciden(rechazo: RechazarIncidencia): Observable<HttpResponse<any>> {
    const url = `${this.rechazar}/rechazar-Incidencia`;
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