using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class ModulosCategoria
{
  [Key]
  public int IDModuloCategoria { get; set; }
  public required string Nombre { get; set; }
  public required string Icono { get; set; }
  public bool Activo { get; set; }

  [JsonIgnore]
  public Collection<Modulo> Modulos { get; set; }
}