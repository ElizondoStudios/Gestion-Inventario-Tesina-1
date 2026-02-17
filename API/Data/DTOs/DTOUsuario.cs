using System;

namespace API.Data.DTOs;

public class DTOUsuario
{
  public int IDUsuario { get; set; }
  public required string Nombre { get; set; }
  public required string Correo { get; set; }
  public bool Activo { get; set; }
  public int IDPerfilPuesto { get; set; }
  public required string DescripcionPerfilPuesto { get; set; }
}
