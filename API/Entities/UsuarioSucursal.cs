using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class UsuarioSucursal
{
  [Key]
  public int IDSucursalUsuario { get; set; }
  public bool Activo { get; set; }

  public required Usuario Usuario { get; set; }
  public required Sucursal Sucursal { get; set; }
}