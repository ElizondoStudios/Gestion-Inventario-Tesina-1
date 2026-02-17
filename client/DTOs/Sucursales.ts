export interface DTOSucursal {
  IDSucursal: number;
  Nombre: string;
  Direccion: string;
  Activo: boolean;
}

export interface DTOCrearSucursal {
  Nombre: string;
  Direccion: string;
}

export interface DTOActualizarSucursal {
  IDSucursal: number;
  Nombre: string;
  Direccion: string;
}
