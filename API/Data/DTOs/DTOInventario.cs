using System;

namespace API.Data.DTOs;

public class DTOInventario
{
  public required string NoParte { get; set; }
  public required string NombreProducto { get; set; }
  public required string DescripcionProducto { get; set; }
  public decimal Precio { get; set; }
  public bool Activo { get; set; }
  public int IDUnidad { get; set; }
  public string? DescripcionUnidad { get; set; }
  public string? AbreviacionUnidad { get; set; }
}
