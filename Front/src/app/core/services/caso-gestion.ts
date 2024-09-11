import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidencia } from '../../interfaces/ViewIndicencia';


@Injectable({
  providedIn: 'root'
})
export class CasoGestion {
  private selectincidencia = 'https://localhost:44346/api/v5/Incidencia/SelectIncidenciasRegistradas';

  constructor(private http: HttpClient) { }


  insertIncidencia(): Observable<ViewIncidencia[]> {
    return this.http.get<ViewIncidencia[]>(`${this.selectincidencia}`);
  }
  
}