using System;

namespace API.Data.DTOs;

public class DTOCrearInventario
{
  public required string NoParte { get; set; }
  public required string NombreProducto { get; set; }
  public required string DescripcionProducto { get; set; }
  public decimal Precio { get; set; }
  public int IDUnidad { get; set; }
}
