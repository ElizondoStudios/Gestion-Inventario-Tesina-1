using System;

namespace API.Data.DTOs;

public class DTOTipoMovimiento
{
  public int IDTipoMovimientoInventario { get; set; }
  public required string Descripcion { get; set; }
  public bool EntradaSalida { get; set; }
}
