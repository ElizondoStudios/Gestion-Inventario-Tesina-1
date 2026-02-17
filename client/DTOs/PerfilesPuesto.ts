export interface DTOPerfilPuesto {
  IDPerfilPuesto: number;
  Descripcion: string;
  Activo: boolean;
}

export interface DTOCrearPerfilPuesto {
  Descripcion: string;
}

export interface DTOActualizarPerfilPuesto {
  IDPerfilPuesto: number;
  Descripcion: string;
}
