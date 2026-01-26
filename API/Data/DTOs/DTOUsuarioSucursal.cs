using System;

namespace API.Data.DTOs;

public class DTOUsuarioSucursal
{
  public int IDSucursalUsuario { get; set; }
  public bool Activo { get; set; }
  public int IDUsuario { get; set; }
  public string NombreUsuario { get; set; }
  public int IDSucursal { get; set; }
  public string NombreSucursal { get; set; }

}
