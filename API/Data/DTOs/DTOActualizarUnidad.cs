using System;

namespace API.Data.DTOs;

public class DTOActualizarUnidad
{
  public int IDUnidad { get; set; }
  public required string Descripcion { get; set; }
  public required string Abreviacion { get; set; }
}
