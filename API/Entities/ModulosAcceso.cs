using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class ModulosAcceso
{
  [Key]
  public int IDModuloAcceso { get; set; }
  
  public required Modulo Modulo { get; set; }
  public required NivelesAcceso NivelAcceso { get; set; }
  public required PerfilPuesto PerfilPuesto { get; set; }
}