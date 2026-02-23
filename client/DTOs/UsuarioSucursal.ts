export interface DTOUsuarioSucursal {
  IDSucursalUsuario: number;
  Activo: boolean;
  IDUsuario: number;
  NombreUsuario: string;
  IDSucursal: number;
  NombreSucursal: string;
}

export interface DTOCrearUsuarioSucursal {
  IDUsuario: number;
  IDSucursal: number;
}
