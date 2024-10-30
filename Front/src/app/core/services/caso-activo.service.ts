import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaSolicitada } from '../../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { EvaluarIncidencia } from '../../interfaces/CasoActivo/EvaluarIndicencia';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';


@Injectable({
  providedIn: 'root'
})
export class Casoactivo{
  private selectincidenciasolicitada = 'https://localhost:44346/api/Incidencia/Historico/consultar-MisSolicitudes';
  private selecttrazabilidad = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-TrazabilidadIncidenciasGeneral';
  private Validar = 'https://localhost:44346/api/Incidencia/Gestion/consultar-estadoResuelto';
  private Evaluación = 'https://localhost:44346/api/Incidencia/Gestion/evaluar_cerrar-Incidencia';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaSolicitada(peGe_Id: number): Observable<ApiResponse<ViewIncidenciaSolicitada>> {
    return this.http.get<ApiResponse<ViewIncidenciaSolicitada>>(`${this.selectincidenciasolicitada}/${peGe_Id}&true`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ApiResponse<ViewTrazabilidadSolicitante>> {
    return this.http.get<ApiResponse<ViewTrazabilidadSolicitante>>(`${this.selecttrazabilidad}/${inci_id}`);
  }

  ValidarIndicencia(inci_id: number): Observable<ApiResponse<string>> {
    return this.http.get<ApiResponse<string>>(`${this.Validar}/${inci_id}`);
  }
  

  enviarEvaluacion(evaluacion: EvaluarIncidencia): Observable<any> {
    return this.http.post(this.Evaluación, evaluacion);
  }


}