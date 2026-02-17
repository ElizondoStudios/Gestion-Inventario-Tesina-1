export interface DTOInventario {
  NoParte: string;
  NombreProducto: string;
  DescripcionProducto: string;
  Precio: number;
  Costo: number;
  Activo: boolean;
  IDUnidad: number;
  DescripcionUnidad?: string;
  AbreviacionUnidad?: string;
}

export interface DTOCrearInventario {
  NoParte: string;
  NombreProducto: string;
  DescripcionProducto: string;
  Precio: number;
  Costo: number;
  IDUnidad: number;
}

export interface DTOActualizarInventario {
  NoParte: string;
  NombreProducto: string;
  DescripcionProducto: string;
  Precio: number;
  Costo: number;
  IDUnidad: number;
}
