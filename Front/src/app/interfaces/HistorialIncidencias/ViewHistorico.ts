export interface ViewHistorico {
    inci_Id: number;
    idContratoSolicitante: number;
    inci_Descripcion: string;
    inci_FechaRegistro: Date;
    caAr_Nombre: string;
    arTe_Nombre: string;
    nombrePrioridad: string;
    idContratoAdmin: number | null;
    inTr_FechaGenerada: Date;
    inci_EstadoActual: number;
}
