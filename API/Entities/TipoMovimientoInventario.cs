using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class TiposMovimientosInventario
{
  [Key]
  public int IDTipoMovimientoInventario { get; set; }
  public required string Descripcion { get; set; }
  public bool EntradaSalida { get; set; }

  [JsonIgnore]
  public Collection<LogInventario> LogInventario { get; set; }
}