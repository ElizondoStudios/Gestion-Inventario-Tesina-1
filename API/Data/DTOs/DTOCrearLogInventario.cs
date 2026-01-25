using System;

namespace API.Data.DTOs;

public class DTOCrearLogInventario
{
  public decimal Cantidad { get; set; }
  public int IDUsuario { get; set; }
  public required string NoParte { get; set; }
  public int IDSucursal { get; set; }
  public int IDTipoMovimiento { get; set; }
}
