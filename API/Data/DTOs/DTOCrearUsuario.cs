using System;

namespace API.Data.DTOs;

public class DTOCrearUsuario
{
  public required string Nombre { get; set; } 
  public required string Correo { get; set; } 
  public required string Contrasenia { get; set; }
  public int IDPerfilPuesto  { get; set; }
}