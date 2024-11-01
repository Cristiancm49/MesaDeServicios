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