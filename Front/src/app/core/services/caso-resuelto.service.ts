import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaSolicitada } from '../../interfaces/CasoActivo/ViewIncidenciaSolicitada';
import { ViewTrazabilidadSolicitante } from '../../interfaces/CasoActivo/Trazabilidad-Solicitante';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';


@Injectable({
  providedIn: 'root'
})
export class Casoresuelto{
  private selectincidenciasolicitada = 'https://localhost:44346/api/Incidencia/Historico/consultar-MisSolicitudes';
  private selecttrazabilidad = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-TrazabilidadIncidenciasGeneral';
  
  constructor(private http: HttpClient) { }


  selectIncidenciaSolicitada(peGe_Id: number): Observable<ApiResponse<ViewIncidenciaSolicitada>> {
    return this.http.get<ApiResponse<ViewIncidenciaSolicitada>>(`${this.selectincidenciasolicitada}/${peGe_Id}&false`);
  }

  selectTrazabilidadSolicitada(inci_id: number): Observable<ApiResponse<ViewTrazabilidadSolicitante>> {
    return this.http.get<ApiResponse<ViewTrazabilidadSolicitante>>(`${this.selecttrazabilidad}/${inci_id}`);
  }

}