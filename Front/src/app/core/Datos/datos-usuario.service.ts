import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DatosUser } from '../../interfaces/CasoRegistro/DatosUser';
import { ApiResponse } from '../../interfaces/Api/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class UserDataStateService {

  private userDataSubject = new BehaviorSubject<DatosUser[] | null>(null);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private errorSubject = new BehaviorSubject<string | null>(null);

  readonly userData$ = this.userDataSubject.asObservable();
  readonly loading$ = this.loadingSubject.asObservable();
  readonly error$ = this.errorSubject.asObservable();

  private apiDatosUsuario = 'https://localhost:44346/api/v1/Oracle/Cotratos_Oracle';

  constructor(private http: HttpClient) {}

  // Método para cargar los datos del usuario
  loadUserData(Documento: number): void {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    this.http.get<ApiResponse<DatosUser>>(`${this.apiDatosUsuario}?documentoIdentidad=${Documento}`).subscribe({
      next: (response) => {
        this.userDataSubject.next(response.data);
        this.loadingSubject.next(false);
      },
      error: (error) => {
        console.error('Error cargando datos del usuario:', error);
        this.errorSubject.next('Error al cargar los datos del usuario');
        this.loadingSubject.next(false);
      }
    });
  }

  // Actualizamos los getters para manejar el array
  get currentUserData(): DatosUser[] | null {
    return this.userDataSubject.getValue();
  }

  get isLoading(): boolean {
    return this.loadingSubject.getValue();
  }

  get error(): string | null {
    return this.errorSubject.getValue();
  }

  // Actualizamos los métodos para obtener valores del primer elemento del array
  getNombreCompleto(): string {
    return this.currentUserData?.[0]?.nombreCompleto || '';
  }

  getDocumentoIdentidad(): string {
    return this.currentUserData?.[0]?.peGe_DocumentoIdentidad || '';
  }

  getUnidadNombre(): string {
    return this.currentUserData?.[0]?.unid_Nombre || '';
  }

  getTelefonoUnidad(): string {
    return this.currentUserData?.[0]?.unid_Telefono || '';
  }

  getCargoDescripcion(): string {
    return this.currentUserData?.[0]?.tnom_Descripcion || '';
  }

  // Método para obtener un usuario específico del array por índice
  getUserByIndex(index: number): DatosUser | null {
    return this.currentUserData?.[index] || null;
  }

  // Método para obtener la cantidad de usuarios
  getUserCount(): number {
    return this.currentUserData?.length || 0;
  }

  // Método para limpiar los datos
  clearData(): void {
    this.userDataSubject.next(null);
    this.errorSubject.next(null);
  }
}
