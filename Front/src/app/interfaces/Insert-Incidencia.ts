export interface Incidencia {
  id_Incidencias: number;
  idSolicitante_Incidencias: number;
  esExc_Incidencias: boolean;
  idAdmin_IncidenciasExc: number | null;
  fechaHora_Incidencias: Date;
  id_AreaTec: number;
  descrip_Incidencias: string;
  eviden_Incidencias: Blob | null;
  valTotal_Incidencias: number;
}
  
