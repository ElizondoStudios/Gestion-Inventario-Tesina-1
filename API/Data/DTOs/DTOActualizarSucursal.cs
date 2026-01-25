using System;

namespace API.Data.DTOs;

public class DTOActualizarSucursal
{
  public int IDSucursal { get; set; }
  public required string Nombre { get; set; }
  public required string Direccion { get; set; }
}
