using System;

namespace API.Data.DTOs;

public class DTOIniciarSesion
{
  public required string Correo { get; set; }
  public required string Contrasenia { get; set; }
}
