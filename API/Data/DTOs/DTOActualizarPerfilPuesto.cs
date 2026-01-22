using System;

namespace API.Data.DTOs;

public class DTOActualizarPerfilPuesto
{
  public int IDPerfilPuesto { get; set; }
  public required string Descripcion { get; set; }
}
