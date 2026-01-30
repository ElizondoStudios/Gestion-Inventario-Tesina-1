using System;

namespace API.Data.DTOs;

public class DTOActualizarSucursalInventario
{
  public int IDSucursalInventario { get; set; }
  public decimal Existencia { get; set; }
  public decimal UmbralExistencia { get; set; }
}
