export interface Incidencia {
  cont_IdSolicitante: number;
  inci_EsExc: boolean;
  usua_IdAdminExc: number | null;
  inci_FechaRegistro: Date;
  arTe_Id: number;
  inci_Descripcion: string;
  inci_ValorTotal: number;
}

