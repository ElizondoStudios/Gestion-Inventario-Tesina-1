using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class SucursalesInventario
{
  [Key]
  public int IDSucursalInventario { get; set; }
  public decimal Existencia { get; set; }
  public decimal UmbralExistencia { get; set; }

  public required Inventario Producto { get; set; }
  public required Sucursal Sucursal { get; set; }
}