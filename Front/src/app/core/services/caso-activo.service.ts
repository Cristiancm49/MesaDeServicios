import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaSolicitada } from '../../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { ValidarEstado } from '../../interfaces/CasoActivo/ValidarEstado';
import { EvaluarIncidencia } from '../../interfaces/CasoActivo/EvaluarIndicencia';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';


@Injectable({
  providedIn: 'root'
})
export class Casoactivo{
  private selectincidenciasolicitada = 'https://localhost:44346/api/Incidencia/Historico/consultar-MisSolicitudes';
  private selecttrazabilidad = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-TrazabilidadIncidencias';
  private Validar = 'https://localhost:44346/api/v5/SolicitanteIncidencia/ValidarEstadoResuelto';
  private Evaluación = 'https://localhost:44346/api/v5/SolicitanteIncidencia';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaSolicitada(peGe_Id: number): Observable<ApiResponse<ViewIncidenciaSolicitada>> {
    return this.http.get<ApiResponse<ViewIncidenciaSolicitada>>(`${this.selectincidenciasolicitada}/${peGe_Id}&true`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ApiResponse<ViewTrazabilidadSolicitante>> {
    return this.http.get<ApiResponse<ViewTrazabilidadSolicitante>>(`${this.selecttrazabilidad}/${inci_id}`);
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