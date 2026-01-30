using System;

namespace API.Data.DTOs;

public class DTOActualizarUsuario
{
  public int IDUsuario  { get; set; }
  public required string Nombre { get; set; } 
  public required string Correo { get; set; } 
  public int IDPerfilPuesto  { get; set; }
}
