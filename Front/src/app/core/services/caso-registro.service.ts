import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AreaTec } from '../../interfaces/area-tec';
import { DatosUser } from '../../interfaces/DatosUser';
import { DatosAdmin } from '../../interfaces/DatosAdmin';
import { Categorias } from '../../interfaces/Interfaz-categoria';
import {Incidencia} from '../../interfaces/Insert-Incidencia';


@Injectable({
  providedIn: 'root'
})
export class CasoRegistroService {
  private apiUrl = 'https://localhost:44346/api/v5/Incidencia/AreasTec';
  private apiDatosUsurio = 'https://localhost:44346/api/v5/Incidencia/SolicitarIncidencias';
  private apiCategorias ="https://localhost:44346/api/v5/Incidencia/CatAreasTec"
  private apiInsertIncidencia = 'https://localhost:44346/api/v5/Incidencia/InsertIncidencias(Normal&Excepcional)';
  private apiDatosAdmin= 'https://localhost:44346/api/v5/Incidencia/InscribirAdmin';

  constructor(private http: HttpClient) { }

  getAreasTec(Id_CatAre: number): Observable<AreaTec[]> {
    return this.http.get<AreaTec[]>(`${this.apiUrl}?Id_CatAre=${Id_CatAre}`);
  }

  getDatosUsuario(peGe_DocumentoIdentidad: number): Observable<DatosUser[]> {
    return this.http.get<DatosUser[]>(`${this.apiDatosUsurio}/${peGe_DocumentoIdentidad}`);
  }

  getDatosAdministrador(peGe_DocumentoIdentidad: number): Observable<DatosAdmin[]> {
    return this.http.get<DatosAdmin[]>(`${this.apiDatosAdmin}/${peGe_DocumentoIdentidad}`);
  }

  getCategorias(): Observable<Categorias[]> {
    return this.http.get<Categorias[]>(`${this.apiCategorias}`);
  }

  insertIncidencia(incidencia: Incidencia): Observable<any> {
    return this.http.post(this.apiInsertIncidencia, incidencia);
  }
  
}