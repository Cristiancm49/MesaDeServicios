import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { insertrol } from '../../interfaces/Rol/insertrol';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';


@Injectable({
  providedIn: 'root'
})
export class Rol{
  private insert = 'https://localhost:44346/api/Incidencia/Usuarios/insertar-Usuarios';
  
  constructor(private http: HttpClient) { }


  insertrol(rol: insertrol): Observable<any> {
    return this.http.post(this.insert, rol);
  }
}