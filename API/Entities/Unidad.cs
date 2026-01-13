using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Unidad
{
  [Key]
  public int IDUnidad { get; set; }
  public required string Descripcion { get; set; }
  public required string Abreviacion { get; set; }
  public bool Activo { get; set; }

  [JsonIgnore]
  public Collection<Inventario> Productos { get; set; }
}