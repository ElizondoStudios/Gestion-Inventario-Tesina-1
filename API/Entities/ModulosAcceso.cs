using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class ModulosAcceso
{
  [Key]
  public int IDModuloAcceso { get; set; }
  public int IDModulo { get; set; }
  public int IDNivelAcceso { get; set; }
  public int IDPerfilPuesto { get; set; }
  
  public Modulo Modulo { get; set; }
  public NivelesAcceso NivelAcceso { get; set; }
  public PerfilPuesto PerfilPuesto { get; set; }
}