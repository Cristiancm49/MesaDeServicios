export interface TrazabilidadHistorico {
    inTr_Id: number;
    inci_Id: number;
    inTr_FechaActualizacion: Date; // Puedes usar Date si prefieres
    inTr_Solucionado: boolean;
    inTr_Escalable: boolean;
    inTr_MotivoRechazo: string | null;
    inTr_Descripcion: string | null;
    rolNombre: string | null;
    atiende: string | null;
    estadoNombre: string;
    inTr_Revisado: boolean;
    inTr_ObservacionAdmin: string | null;
}
