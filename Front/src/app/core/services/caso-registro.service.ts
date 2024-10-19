import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AreaTec } from '../../interfaces/CasoRegistro/area-tec';
import { DatosUser } from '../../interfaces/CasoRegistro/DatosUser';
import { Categorias } from '../../interfaces/CasoRegistro/Interfaz-categoria';
import { Incidencia } from '../../interfaces/CasoRegistro/Insert-Incidencia';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class CasoRegistroService {
  private apiUrl = 'https://localhost:44346/api/Incidencia/Insertar/consultar-AreaTecnica';
  private apiDatosUsurio = 'https://localhost:44346/api/v1/Oracle/Cotratos_Oracle';
  private apiCategorias ="https://localhost:44346/api/Incidencia/Insertar/consultar-CategoriaAreaTecnica"
  private apiInsertIncidencia = 'https://localhost:44346/api/Incidencia/Insertar/insertar-Incidencia';

  constructor(private http: HttpClient) { }

  getAreasTec(Id_CatAre: number): Observable<ApiResponse<AreaTec>> {
    return this.http.get<ApiResponse<AreaTec>>(`${this.apiUrl}/${Id_CatAre}`);
  }

  getDatosUsuario(peGe_DocumentoIdentidad: number): Observable<ApiResponse<DatosUser>> {
    return this.http.get<ApiResponse<DatosUser>>(`${this.apiDatosUsurio}?documentoIdentidad=${peGe_DocumentoIdentidad}`);
  }

  getDatosAdministrador(peGe_DocumentoIdentidad: number): Observable<ApiResponse<DatosUser>> {
    return this.http.get<ApiResponse<DatosUser>>(`${this.apiDatosUsurio}/${peGe_DocumentoIdentidad}`);
  }

  getCategorias(): Observable<ApiResponse<Categorias>> {
    return this.http.get<ApiResponse<Categorias>>(`${this.apiCategorias}`);
  }

  insertIncidencia(incidencia: Incidencia): Observable<any> {
    return this.http.post(this.apiInsertIncidencia, incidencia);
  }
}
