using System;

namespace API.Data.DTOs;

public class DTOSucursalInventario
{
  public int IDSucursalInventario { get; set; }
  public decimal Existencia { get; set; }
  public decimal UmbralExistencia { get; set; }
  public required string NoParte { get; set; }
  public string NombreProducto { get; set; }
  public string Unidad { get; set; }
  public int IDSucursal { get; set; }
  public string NombreSucursal { get; set; }
}
