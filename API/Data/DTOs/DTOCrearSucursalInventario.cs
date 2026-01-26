using System;

namespace API.Data.DTOs;

public class DTOCrearSucursalInventario
{
  public decimal Existencia { get; set; }
  public decimal UmbralExistencia { get; set; }
  public required string NoParte { get; set; }
  public int IDSucursal { get; set; }
}
