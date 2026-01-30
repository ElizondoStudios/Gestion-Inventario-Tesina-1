using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class UsuarioSucursal
{
  [Key]
  public int IDSucursalUsuario { get; set; }
  public bool Activo { get; set; }
  public int IDUsuario { get; set; }
  public int IDSucursal { get; set; }

  public Usuario Usuario { get; set; }
  public Sucursal Sucursal { get; set; }
}