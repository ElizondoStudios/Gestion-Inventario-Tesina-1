using System;

namespace API.Data.DTOs;

public class VentaInicio
{
  public decimal Total { get; set; } 
  public DateTime Fecha { get; set; } 
}

public class CompraInicio
{
  public decimal Total { get; set; } 
  public DateTime Fecha { get; set; } 
}

public class DTOVentasVsComprasInicio
{
  public required List<VentaInicio> Ventas { get; set; }
  public required List<CompraInicio> Compras{ get; set; }
}
