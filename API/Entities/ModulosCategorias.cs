using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class ModulosCategorias
{
  [Key]
  public int IDModuloCategoria { get; set; }
  public string Nombre { get; set; }
  public string Icono { get; set; }
  public bool Activo { get; set; }

  [JsonIgnore]
  public Collection<Modulos> Modulos { get; set; }
}