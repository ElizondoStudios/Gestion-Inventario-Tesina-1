using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class PerfilPuesto
{
  [Key]
  public int IDPerfilPuesto { get; set; }
  public required string Descripcion { get; set; }
  public bool Activo { get; set; }

  [JsonIgnore]
  public Collection<Usuario> Usuarios { get; set; }
  [JsonIgnore]
  public Collection<ModulosAcceso> ModulosAcceso { get; set; }
}