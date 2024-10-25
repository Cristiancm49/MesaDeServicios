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
  private TipoSolucion = 'https://localhost:44346/api/v5/DiagnosticosIncidencia/TipoSolucionDiagnosticos';
  private Diagnostic = 'https://localhost:44346/api/v5/DiagnosticosIncidencia';

  constructor(private http: HttpClient) { }


  selectIncidenciaasignada(IdContrato: number): Observable<ApiResponse<ViewIncidenciaAsignada>> {
    return this.http.get<ApiResponse<ViewIncidenciaAsignada>>(`${this.selectincidenciaasignada}/${IdContrato}`);
  }



  TipoS(): Observable<ViewTipoSoluciones[]> {
    return this.http.get<ViewTipoSoluciones[]>(`${this.TipoSolucion}`); 
  }

  insertDiagnostico(Diagnostico: InsertDiagnostico): Observable<HttpResponse<any>> {
    const url = `${this.Diagnostic}/GenerarDiagnosticos`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(url, Diagnostico, { 
      headers: headers, 
      observe: 'response', 
      responseType: 'text' 
    });
  }


}