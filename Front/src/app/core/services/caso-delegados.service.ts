import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewIncidenciaAsignada } from '../../interfaces/CasoDelegado/ViewIncidenciaAsignada';
import { ViewTipoSoluciones } from '../../interfaces/CasoDelegado/ViewTipoSoluciones';
import { InsertDiagnostico } from '../../interfaces/CasoDelegado/InsertDiagnostico';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';


@Injectable({
  providedIn: 'root'
})
export class Casodelegado{
  private selectincidenciaasignada = 'https://localhost:44346/api/Incidencia/Trazabilidad/consultar-MisIncidenciasActivas';
  private TipoSolucion = 'https://localhost:44346/api/Incidencia/Trazabilidad/consultar-TiposDeSolucion';
  private Diagnostic = 'https://localhost:44346/api/Incidencia/Trazabilidad/generar-Diagnostico';

  constructor(private http: HttpClient) { }


  selectIncidenciaasignada(IdContrato: number): Observable<ApiResponse<ViewIncidenciaAsignada>> {
    return this.http.get<ApiResponse<ViewIncidenciaAsignada>>(`${this.selectincidenciaasignada}/${IdContrato}`);
  }



  TipoS(): Observable<ApiResponse<ViewTipoSoluciones>> {
    return this.http.get<ApiResponse<ViewTipoSoluciones>>(`${this.TipoSolucion}`); 
  }


  insertDiagnostico(Diagnostico: InsertDiagnostico): Observable<any> {
    return this.http.post(this.Diagnostic, Diagnostico);
  }


}