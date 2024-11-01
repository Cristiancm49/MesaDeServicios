export interface InsertDiagnostico{

        inci_Id: number,
        idContratoUsuario: number,
        diag_DescripcionDiagnostico: string,
        diag_Solucionado: boolean,
        tiSo_Id: number | null,
        diag_Escalable: boolean
      
}