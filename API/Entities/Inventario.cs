using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Inventario
{
  [Key]
  public required string NoParte { get; set; }
  public required string NombreProducto { get; set; }
  public required string DescripcionProducto { get; set; }
  public decimal Precio { get; set; }
  public bool Activo { get; set; }

  public required Unidad Unidad { get; set; }
  [JsonIgnore]
  public Collection<SucursalesInventario> SucursalesInventario { get; set; }
  [JsonIgnore]
  public Collection<LogInventario> LogInventario { get; set; }
}