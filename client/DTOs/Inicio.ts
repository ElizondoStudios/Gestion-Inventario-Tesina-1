export interface DTOTotalesInicio {
  Ventas: number;
  Compras: number;
  Merma: number;
}

export interface DTOMovimientosRecientesInicio {
  DescripcionTipoMoviento: string;
  NombreProducto: string;
  Cantidad: number;
  DescripcionUnidad: string;
  Fecha: Date;
  EntradaSalida: boolean;
}

export interface DTOVentasVsComprasInicio {
  Ventas: { Total: number; Fecha: Date }[];
  Compras: { Total: number; Fecha: Date }[];
}

export interface DTOAlertasInventarioInicio {
  Existencia: number;
  UmbralExistencia: number;
  NoParte: string;
  NombreSucursal: string;
}
