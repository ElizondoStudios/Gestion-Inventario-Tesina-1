using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Modulos
{
  [Key]
  public int IDModulo { get; set; }
  public string Nombre { get; set; }
  public string Icono { get; set; }
  public bool Activo { get; set; }

  public ModulosCategorias ModuloCategoria { get; set; }
  [JsonIgnore]
  public Collection<ModulosAcceso> ModulosAcceso { get; set; }
}