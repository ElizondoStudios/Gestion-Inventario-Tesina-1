using System;

namespace API.Data.DTOs;

public class DTOPerfilPuesto
{
  public int IDPerfilPuesto { get; set; }
  public required string Descripcion { get; set; }
  public bool Activo { get; set; }
}
