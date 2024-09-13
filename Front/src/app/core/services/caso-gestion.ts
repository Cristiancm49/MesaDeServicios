import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidencia } from '../../interfaces/ViewIndicencia';
import { ViewPersonalAsignacion } from '../../interfaces/ViewPersonalAsignacion';
import { InsertAsignacion } from '../../interfaces/Insert-Asignacion';
import { ViewRoles } from '../../interfaces/ViewRoles';
import { RechazarIncidencia } from '../../interfaces/RechazarIncidencia';


@Injectable({
  providedIn: 'root'
})
export class CasoGestion {
  private selectincidencia = 'https://localhost:44346/api/v5/Incidencia/SelectIncidenciasRegistradas';
  private selectpersonal = 'https://localhost:44346/api/v5/GestionIncidencia/UsuariosConIncidenciasAsignadas';
  private insertarasignacion = 'https://localhost:44346/api/v5/GestionIncidencia/AsignarUsuario';
  private selectroles = 'https://localhost:44346/api/v2/Roles/RolesModulo';
  private rechazar = 'https://localhost:44346/api/v5/GestionIncidencia/RechazarIncidencia';

  constructor(private http: HttpClient) { }


  insertIncidencia(): Observable<ViewIncidencia[]> {
    return this.http.get<ViewIncidencia[]>(`${this.selectincidencia}`);
  }

  mostrarpersonal(id_Rol: number): Observable<ViewPersonalAsignacion[]> {
    return this.http.get<ViewPersonalAsignacion[]>(`${this.selectpersonal}?id_Rol=${id_Rol}`);
  }

  insertasignacion(asignacion: InsertAsignacion): Observable<any> {
    return this.http.post(this.insertarasignacion, asignacion);
  }

  getRoles(): Observable<ViewRoles[]> {
    return this.http.get<ViewRoles[]>(`${this.selectroles}`);
  }

  rechazarinciden(rechazo: RechazarIncidencia): Observable<any> {
    return this.http.post(this.rechazar, rechazo)
  }
}