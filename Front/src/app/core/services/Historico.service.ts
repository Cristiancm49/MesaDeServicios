import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewHistorico } from '../../interfaces/HistorialIncidencias/ViewHistorico';
import { TrazabilidadHistorico } from '../../interfaces/HistorialIncidencias/ViewTrazabilidad';


@Injectable({
  providedIn: 'root'
})
export class Historico{
  private selecthistorico = 'https://localhost:44346/api/v5/ReportesIncidencia/HistoricoIncidencias';
  private selecttrazabilidad = 'https://localhost:44346/api/v5/TrazabilidadIncidencia/VistaTrazabilidadAdmin';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaHistorica(): Observable<ViewHistorico[]> {
    return this.http.get<ViewHistorico[]>(`${this.selecthistorico}`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<TrazabilidadHistorico[]> {
    return this.http.get<TrazabilidadHistorico[]>(`${this.selecttrazabilidad}?inci_id=${inci_id}`);
  }
}