export interface DTOModulo{
  IDModulo: number;
  Nombre: string;
  Icono: string;
}

export interface DTONivel{
  NivelAcceso: number;
  Descripcion: number;
}

export interface DTOModulosAcceso{
  IDModuloAcceso: number;
  IDModulo: number;
  NombreModulo: string;
  IconoModulo: string;
  DescripcionNivelAcceso: string;
  DescripcionPerfilPuesto: string;
  NivelAcceso: number;
  IDPerfilPuesto: number;
  IDModuloCategoria: number;
  NombreModuloCategoria: string;
  IconoModuloCategoria: string;
}

export interface DTORegistrarAccesoModulo{
  IDModulo: number;
  IDNivelAcceso: number;
  IDPerfilPuesto: number;
}