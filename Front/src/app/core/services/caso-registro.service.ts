import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface AreaTec {
  id_AreaTec: number;
  nom_AreaTec: string;
  val_AreaTec: number;
}

@Injectable({
  providedIn: 'root'
})
export class CasoRegistroService {
  private apiUrl = 'https://localhost:44346/api/v3/ConsultaArea';

  constructor(private http: HttpClient) { }

  getAreasTec(docChaLog: number): Observable<AreaTec[]> {
    return this.http.get<AreaTec[]>(`${this.apiUrl}?docChaLog=${docChaLog}`);
  }
}