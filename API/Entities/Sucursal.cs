using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Sucursal
{
  [Key]
  public int IDSucursal { get; set; }
  public required string Nombre { get; set; }
  public required string Direccion { get; set; }
  public bool Activo { get; set; }

  [JsonIgnore]
  public Collection<UsuarioSucursal> UsuarioSucursales { get; set; }
  [JsonIgnore]
  public Collection<SucursalesInventario> SucursalesInventario { get; set; }
  [JsonIgnore]
  public Collection<LogInventario> LogInventario { get; set; }

}