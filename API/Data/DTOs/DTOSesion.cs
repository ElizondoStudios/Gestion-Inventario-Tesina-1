using System;
using System.Diagnostics.CodeAnalysis;

namespace API.Data.DTOs;

public class DTOSesion
{
  public int IDUsuario { get; set; }
  public required string Token { get; set; }
}
