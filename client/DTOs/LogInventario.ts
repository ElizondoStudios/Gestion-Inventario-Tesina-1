export interface DTOCrearLogInventario{
  Cantidad: number;
  IDUsuario: number;
  NoParte: string;
  IDSucursal: number;
  IDTipoMovimiento: number;
}

export interface DTOLogInventario{
  IDLogInventario: number;
  Fecha: Date;
  Cantidad: number;
  IDUsuario: number;
  NoParte: string;
  IDSucursal: number;
  Sucursal: string;
  IDTipoMovimiento: number;
  DescripcionTipoMovimiento: string;
}

export interface DTOTipoMovimiento {
  IDTipoMovimientoInventario: number;
  Descripcion: string;
  EntradaSalida: boolean;
}