using System;

namespace API.Data.DTOs;

public class DTOModulosAcceso
{
  public int IDModuloAcceso { get; set; }
  public int IDModulo { get; set; }
  public string NombreModulo { get; set; }
  public string DescripcionNivelAcceso { get; set; }
  public string DescripcionPerfilPuesto { get; set; }
  public int NivelAcceso { get; set; }
  public int IDPerfilPuesto { get; set; }
}
