using System;

namespace API.Data.DTOs;

public class DTOModulo
{
  public int IDModulo { get; set; }
  public required string Nombre { get; set; }
  public required string Icono { get; set; }
}
