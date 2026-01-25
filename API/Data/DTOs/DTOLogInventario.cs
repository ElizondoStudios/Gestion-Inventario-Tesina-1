using System;

namespace API.Data.DTOs;

public class DTOLogInventario
{
  public int IDLogInventario { get; set; }
  public DateTime Fecha { get; set; }
  public decimal Cantidad { get; set; }
  public int IDUsuario { get; set; }
  public required string NoParte { get; set; }
  public int IDSucursal { get; set; }
  public string Sucursal { get; set; }
  public int IDTipoMovimiento { get; set; }
  public string DescripcionTipoMovimiento { get; set; }
}
