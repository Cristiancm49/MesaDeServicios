import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AreaTec } from '../../interfaces/area-tec';
import { DatosUser } from '../../interfaces/DatosUser';


@Injectable({
  providedIn: 'root'
})
export class CasoRegistroService {
  private apiUrl = 'https://localhost:44346/api/v3/ConsultaArea';
  private apiDatosUsurio = 'https://localhost:44346/api/v3/PruebaRoles/ConsultarInfoChaira';

  constructor(private http: HttpClient) { }

  getAreasTec(docChaLog: number): Observable<AreaTec[]> {
    return this.http.get<AreaTec[]>(`${this.apiUrl}?docChaLog=${docChaLog}`);
  }

  getDatosUsuario(docChaLog: number): Observable<DatosUser[]> {
    return this.http.get<DatosUser[]>(`${this.apiDatosUsurio}?docChaLog=${docChaLog}`);
  }
  
}