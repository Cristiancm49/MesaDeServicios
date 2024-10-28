import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ViewSeguimiento } from '../../interfaces/CasoSeguimiento/viewseguimiento';
import { ViewReporte } from '../../interfaces/CasoSeguimiento/Viewreportes';
import { Insertaceptacion } from '../../interfaces/CasoSeguimiento/Aceptar';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';



@Injectable({
  providedIn: 'root'
})
export class Seguimiento{
  private selectsegui = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-SeguimientoIncidencias';
  private selectreporte = 'https://localhost:44346/api/Incidencia/Seguimiento/consultar-TrazabilidadIncidencias';
  private aceptar = 'https://localhost:44346/api/Incidencia/Gestion/resolver-Incidencia';
  
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

}