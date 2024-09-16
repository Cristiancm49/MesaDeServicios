import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaSolicitada } from '../../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { ValidarEstado } from '../../interfaces/CasoActivo/ValidarEstado';
import { EvaluarIncidencia } from '../../interfaces/CasoActivo/EvaluarIndicencia';


@Injectable({
  providedIn: 'root'
})
export class Casoactivo{
  private selectincidenciasolicitada = 'https://localhost:44346/api/v5/SolicitanteIncidencia/VistaIncidenciasFuncionario';
  private selecttrazabilidad = 'https://localhost:44346/api/v5/SolicitanteIncidencia/VistaTrazabilidadFuncionario';
  private Validar = 'https://localhost:44346/api/v5/SolicitanteIncidencia/ValidarEstadoResuelto';
  private Evaluación = 'https://localhost:44346/api/v5/SolicitanteIncidencia/CerrarIncidencia(SinActualizarPromedioUsuario)';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaSolicitada(documento: number): Observable<ViewIncidenciaSolicitada[]> {
    return this.http.get<ViewIncidenciaSolicitada[]>(`${this.selectincidenciasolicitada}?documento=${documento}`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ViewTrazabilidadSolicitante[]> {
    return this.http.get<ViewTrazabilidadSolicitante[]>(`${this.selecttrazabilidad}?inci_id=${inci_id}`);
  }

  ValidarIndicencia(inci_id: number): Observable<ValidarEstado[]> {
    return this.http.get<ValidarEstado[]>(`${this.Validar}?inci_id=${inci_id}`);
  }

  enviarEvaluacion(evaluacion: EvaluarIncidencia): Observable<HttpResponse<any>> {
    const url = `${this.Evaluación}/CerrarIncidencia(SinActualizarPromedioUsuario)`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, evaluacion, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }


}