export interface DTOIniciarSesion {
  Correo: string;
  Contrasenia: string 
}

export interface DTOSesion {
  IDUsuario: number;
  Token: string;
  Nombre: string;
}