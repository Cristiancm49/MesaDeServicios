export interface ViewIncidenciaAsignada {
    inci_Id: number;
    solicitante_NombreCompleto: string;
    solicitante_DocumentoIdentidad: number;
    cont_Cargo: string;
    unid_Nombre: string;
    unid_Telefono: number;
    categoriaAreaTecnica_Nombre: string;
    areaTecnica_Nombre: string;
    inci_Descripcion: string;
    inci_FechaRegistro: string;
    admin_NombreCompleto: string | null;
    inci_ValorTotal: number;
    prioridad_Tipo: string;
    inci_UltimoEstado: number;
  }
  