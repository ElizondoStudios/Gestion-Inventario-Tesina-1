using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Entities;

public class Usuario
{
  [Key]
  public int IDUsuario { get; set; }
  public string Nombre { get; set; }
  public string Correo { get; set; }
  public byte[] ContraseniaHash { get; set; }
  public byte[] ContraseniaSalt { get; set; }
  public bool Activo { get; set; }

  public PerfilPuesto PerfilPuesto { get; set; }
}