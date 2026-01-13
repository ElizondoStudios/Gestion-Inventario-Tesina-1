using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class LogInventario
{
  [Key]
  public int IDLogInventario { get; set; }
  public DateTime Fecha { get; set; }
  public decimal Cantidad { get; set; }

  public required Usuario QuienRealiza {get; set;}
  public required Inventario Producto {get; set;}
  public required Sucursal Sucursal { get; set; }
  public required TiposMovimientosInventario TipoMovimiento { get; set; }
}