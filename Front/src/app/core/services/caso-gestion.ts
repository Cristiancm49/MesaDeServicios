import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidencia } from '../../interfaces/ViewIndicencia';
import { ViewPersonalAsignacion } from '../../interfaces/ViewPersonalAsignacion';
import { InsertAsignacion } from '../../interfaces/Insert-Asignacion';


@Injectable({
  providedIn: 'root'
})
export class CasoGestion {
  private selectincidencia = 'https://localhost:44346/api/v5/Incidencia/SelectIncidenciasRegistradas';
  private selectpersonal = 'https://localhost:44346/api/v5/GestionIncidencia/UsuariosConIncidenciasAsignadas';
  private insertarasignacion = 'https://localhost:44346/api/v5/GestionIncidencia/AsignarUsuario';

  constructor(private http: HttpClient) { }


  insertIncidencia(): Observable<ViewIncidencia[]> {
    return this.http.get<ViewIncidencia[]>(`${this.selectincidencia}`);
  }
  
  
  mostrarpersonal(): Observable<ViewPersonalAsignacion[]> {
    return this.http.get<ViewPersonalAsignacion[]>(`${this.selectpersonal}`);
  }

  insertasignacion(asignacion: InsertAsignacion): Observable<any> {
    return this.http.post(this.insertarasignacion, asignacion);
  }
}