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
  NombreModulo: number;
  DescripcionNivelAcceso: string;
  DescripcionPerfilPuesto: string;
  NivelAcceso: number;
  IDPerfilPuesto: number;
}

export interface DTORegistrarAccesoModulo{
  IDModulo: number;
  IDNivelAcceso: number;
  IDPerfilPuesto: number;
}