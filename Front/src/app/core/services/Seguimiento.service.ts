import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewSeguimiento } from '../../interfaces/CasoSeguimiento/viewseguimiento';
import { ViewReporte } from '../../interfaces/CasoSeguimiento/Viewreportes';
import { Insertaceptacion } from '../../interfaces/CasoSeguimiento/Aceptar';
import { InsertAsignacion } from '../../interfaces/CasoGestión/Insert-Asignacion';
import { InsertEscaladoExterno } from '../../interfaces/CasoSeguimiento/InsertExterno';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';



@Injectable({
  providedIn: 'root'
})
export class Seguimiento{
  private selectsegui = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-SeguimientoIncidencias';
  private selectreporte = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-TrazabilidadIncidencias';
  private aceptar = 'https://localhost:44346/api/Incidencia/Gestion/resolver-Incidencia';
  private escalar = 'https://localhost:44346/api/Incidencia/Trazabilidad/escalarInterno-Incidencia';
  private escalarexterno = 'https://localhost:44346/api/Incidencia/Trazabilidad/escalarExterno-Incidencia';
  
  constructor(private http: HttpClient) { }


  selectseguimiento(): Observable<ApiResponse<ViewSeguimiento>> {
    return this.http.get<ApiResponse<ViewSeguimiento>>(`${this.selectsegui}`);
  }

  selectdiagnostico(IdInci: number): Observable<ApiResponse<ViewReporte>> {
    return this.http.get<ApiResponse<ViewReporte>>(`${this.selectreporte}/${IdInci}`);
  }

  insertDiagnostico(Diagnostico: Insertaceptacion): Observable<any> {
    return this.http.post(this.aceptar, Diagnostico);
  }

  insertEscalado(asignado: InsertAsignacion): Observable<any> {
    return this.http.post(this.escalar, asignado);
  }

  insertEscaladoExterno(externo: InsertEscaladoExterno): Observable<any> {
    return this.http.post(this.escalarexterno, externo);
  }

}