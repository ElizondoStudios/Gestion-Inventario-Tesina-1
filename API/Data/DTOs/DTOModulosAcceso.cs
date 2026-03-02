using System;

namespace API.Data.DTOs;

public class DTOModulosAcceso
{
  public int IDModuloAcceso { get; set; }
  public int IDModulo { get; set; }
  public required string NombreModulo { get; set; }
  public required string IconoModulo { get; set; }
  public required string DescripcionNivelAcceso { get; set; }
  public required string DescripcionPerfilPuesto { get; set; }
  public int NivelAcceso { get; set; }
  public int IDPerfilPuesto { get; set; }
  public int IDModuloCategoria { get; set; }
  public required string NombreModuloCategoria { get; set; }
  public required string IconoModuloCategoria { get; set; }
}
