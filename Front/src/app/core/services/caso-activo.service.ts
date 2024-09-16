import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaSolicitada } from '../../interfaces/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../../interfaces/Trazabilidad-Solicitante';


@Injectable({
  providedIn: 'root'
})
export class Casoactivo{
  private selectincidenciasolicitada = 'https://localhost:44346/api/v5/SolicitanteIncidencia/VistaIncidenciasFuncionario';
  private selecttrazabilidad = 'https://localhost:44346/api/v5/SolicitanteIncidencia/VistaTrazabilidadFuncionario';

  constructor(private http: HttpClient) { }


  selectIncidenciaSolicitada(documento: number): Observable<ViewIncidenciaSolicitada[]> {
    return this.http.get<ViewIncidenciaSolicitada[]>(`${this.selectincidenciasolicitada}?documento=${documento}`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ViewTrazabilidadSolicitante[]> {
    return this.http.get<ViewTrazabilidadSolicitante[]>(`${this.selecttrazabilidad}?inci_id=${inci_id}`);
  }


}