import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MiHistorico } from '../../interfaces/CasoFinalizado/MiHistorico';
import { ViewTrazabilidadSolicitante } from '../../interfaces/CasoActivo/Trazabilidad-Solicitante';


@Injectable({
  providedIn: 'root'
})
export class Casofinalizado{
  private selectincidenciami = 'https://localhost:44346/api/v5/AsignadasIncidencia/MiHistoricoIncidencias';
  private selecttrazabilidad = 'https://localhost:44346/api/v5/SolicitanteIncidencia/VistaTrazabilidadFuncionario';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaHistorica(documento: number): Observable<MiHistorico[]> {
    return this.http.get<MiHistorico[]>(`${this.selectincidenciami}?documentoIdentidad=${documento}`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ViewTrazabilidadSolicitante[]> {
    return this.http.get<ViewTrazabilidadSolicitante[]>(`${this.selecttrazabilidad}?inci_id=${inci_id}`);
  }
}