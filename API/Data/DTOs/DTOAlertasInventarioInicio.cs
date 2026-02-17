using System;

namespace API.Data.DTOs;

public class DTOAlertasInventarioInicio
{
  public decimal Existencia { get; set; }
  public decimal UmbralExistencia { get; set; }
  public required string NoParte { get; set; }
  public required string NombreSucursal { get; set; }
}
