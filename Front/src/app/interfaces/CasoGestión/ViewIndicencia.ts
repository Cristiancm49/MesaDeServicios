export interface ViewIncidencia {
  inci_Id: number;
  nombreCompletoSolicitante: string;
  cont_Cargo: string;
  unid_Nombre: string;
  unid_Telefono: number;
  caAr_Nombre: string;
  arTe_Nombre: string;
  inci_Descripcion: string;
  inTr_FechaGenerada: string;
  nombrePrioridad: string;
  nombreCompletoAdmin?: string | null;
}
