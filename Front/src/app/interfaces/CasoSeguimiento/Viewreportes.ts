export interface ViewReporte {
    inTr_Id: number;
    inTr_FechaGenerada: Date;
    diag_Descripcion: string | null;
    diag_Solucionado: boolean;
    tiSo_Id: number | null;
    tiSo_Nombre: string;
    tiSo_Descripcion: string;
    diag_Escalable: boolean;
    trEs_Id: number;
    trEs_Nombre: string;
    trEs_Descripcion: string;
    usua_Id: number;
    contratoUsuario: number;
    nombreRol: string;
  }