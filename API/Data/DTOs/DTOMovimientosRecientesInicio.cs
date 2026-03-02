using System;

namespace API.Data.DTOs;

public class DTOMovimientosRecientesInicio
{
  public required string DescripcionTipoMoviento { get; set; }
  public required string NombreProducto { get; set; }
  public decimal Cantidad { get; set; }
  public required string DescripcionUnidad { get; set; }
  public required DateTime Fecha { get; set; }
  public bool EntradaSalida { get; set; }
}
