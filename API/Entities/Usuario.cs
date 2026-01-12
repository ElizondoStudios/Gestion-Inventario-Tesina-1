using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Usuario
{
  [Key]
  public int IDUsuario { get; set; }
  public string Nombre { get; set; }
  public string Correo { get; set; }
  public byte[] ContraseniaHash { get; set; }
  public byte[] ContraseniaSalt { get; set; }
  public int IDPerfilPuesto { get; set; }
  public bool Activo { get; set; }
}