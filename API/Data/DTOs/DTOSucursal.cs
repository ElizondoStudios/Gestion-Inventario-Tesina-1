using System;

namespace API.Data.DTOs;

public class DTOSucursal
{
  public int IDSucursal { get; set; }
  public required string Nombre { get; set; }
  public required string Direccion { get; set; }
  public bool Activo { get; set; }
}
