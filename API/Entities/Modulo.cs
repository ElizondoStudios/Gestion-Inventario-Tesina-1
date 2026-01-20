using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Modulo
{
  [Key]
  public int IDModulo { get; set; }
  public required string Nombre { get; set; }
  public required string Icono { get; set; }
  public bool Activo { get; set; }
  public int IDModuloCategoria { get; set; }

  public ModulosCategoria ModuloCategoria { get; set; }
  [JsonIgnore]
  public Collection<ModulosAcceso> ModulosAcceso { get; set; }
}