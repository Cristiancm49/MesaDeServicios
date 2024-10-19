export interface DatosUser {
  peGe_Id: number; // ID de persona general
  peGe_DocumentoIdentidad: string; // Documento de identidad
  nombreCompleto: string; // Nombre completo
  unid_Id: number; // ID de la unidad
  unid_Nombre: string; // Nombre de la unidad
  unid_Telefono: string; // Teléfono de la unidad
  unid_ExtTelefono: string; // Extensión telefónica de la unidad
  unid_Nivel: string; // Nivel de la unidad
  cont_Id: number; // ID de contacto
  cont_Numero: number | null; // Número de contacto (puede ser nulo)
  tnom_Descripcion: string; // Descripción del cargo
  cont_FechaInicio: Date; // Fecha de inicio del contrato
  fechaFinContrato: Date | null; // Fecha de fin del contrato (puede ser nulo)
  cont_EstadoContrato: string; // Estado del contrato
}

