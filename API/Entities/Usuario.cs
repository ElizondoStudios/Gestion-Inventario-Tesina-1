using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Entities;

public class Usuario
{
  [Key]
  public int IDUsuario { get; set; }
  public required string Nombre { get; set; }
  public required string Correo { get; set; }
  public required byte[] ContraseniaHash { get; set; }
  public required byte[] ContraseniaSalt { get; set; }
  public bool Activo { get; set; }
  public int IDPerfilPuesto { get; set; }

  public PerfilPuesto PerfilPuesto { get; set; }
  [JsonIgnore]
  public Collection<UsuarioSucursal> UsuarioSucursales { get; set; }
  [JsonIgnore]
  public Collection<LogInventario> LogInventario { get; set; }
}