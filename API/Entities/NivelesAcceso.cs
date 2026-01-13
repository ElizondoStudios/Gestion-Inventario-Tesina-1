using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class NivelesAcceso
{
  [Key]
  public int NivelAcceso { get; set; }
  public string Descripcion { get; set; }

  [JsonIgnore]
  public Collection<ModulosAcceso> ModulosAcceso { get; set; }
}