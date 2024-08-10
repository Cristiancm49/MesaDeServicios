import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AreaTec } from '../../interfaces/area-tec';
import { DatosUser } from '../../interfaces/DatosUser';
import { Categorias } from '../../interfaces/Interfaz-categoria';
import {Incidencia} from '../../interfaces/Insert-Incidencia';


@Injectable({
  providedIn: 'root'
})
export class CasoRegistroService {
  private apiUrl = 'https://localhost:44346/api/v1/ConsultaArea';
  private apiDatosUsurio = 'https://localhost:44346/api/v5/Incidencia/SelectSolicitante';
  private apiCategorias ="https://localhost:44346/api/v3/ConsultaCategoria"
  private apiInsertIncidencia = 'https://localhost:44346/api/v5/Incidencia/InsertIncidencia';

  constructor(private http: HttpClient) { }

  getAreasTec(Id_CatAre: number): Observable<AreaTec[]> {
    return this.http.get<AreaTec[]>(`${this.apiUrl}?Id_CatAre=${Id_CatAre}`);
  }

  getDatosUsuario(docChaLog: number): Observable<DatosUser[]> {
    return this.http.get<DatosUser[]>(`${this.apiDatosUsurio}?docChaLog=${docChaLog}`);
  }

  getCategorias(docChaLog: number): Observable<Categorias[]> {
    return this.http.get<Categorias[]>(`${this.apiCategorias}?docChaLog=${docChaLog}`);
  }

  insertIncidencia(incidencia: Incidencia): Observable<any> {
    return this.http.post(this.apiInsertIncidencia, incidencia);
  }
  
}