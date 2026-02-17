export interface DTOUsuario
{
  IDUsuario: number;
  Nombre: string;
  Correo: string;
  Activo: boolean;
  IDPerfilPuesto: number;
  DescripcionPerfilPuesto: string;
}

export interface DTOCrearUsuario
{
  Nombre: string;
  Correo: string;
  Contrasenia: string;
  IDPerfilPuesto: number;
}

export interface DTOActualizarUsuario
{
  IDUsuario: number;
  Nombre: string;
  Correo: string;
  IDPerfilPuesto: number;
}