export interface AreaTec {
  arTe_Id: number;
  arTe_Nombre: string;
}

export interface ApiResponse<T> {
  status: string;
  answer: any;
  statusCode: number;
  errors: any[];
  data: T[];
  timestamp: string;
  requestId: string;
  localizedMessage: string | null;
}

