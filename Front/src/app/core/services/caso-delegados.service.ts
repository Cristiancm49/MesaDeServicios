import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaAsignada } from '../../interfaces/CasoDelegado/ViewIncidenciaAsignada';


@Injectable({
  providedIn: 'root'
})
export class Casodelegado{
  private selectincidenciaasignada = 'https://localhost:44346/api/v5/AsignadasIncidencia/MisIncidenciasAsignadas';

  constructor(private http: HttpClient) { }


  selectIncidenciaasignada(documentoIdentidad: number): Observable<ViewIncidenciaAsignada[]> {
    return this.http.get<ViewIncidenciaAsignada[]>(`${this.selectincidenciaasignada}?documentoIdentidad=${documentoIdentidad}`);
  }

}