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
  public int IDUsuario { get; set; }
  public required string NoParte { get; set; }
  public int IDSucursal { get; set; }
  public int IDTipoMovimiento { get; set; }

  public Usuario QuienRealiza {get; set;}
  public Inventario Producto {get; set;}
  public Sucursal Sucursal { get; set; }
  public TiposMovimientosInventario TipoMovimiento { get; set; }
}