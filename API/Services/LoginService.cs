using System;
using System.Security.Cryptography;
using System.Text;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class LoginService(IUsuariosRepository usuariosRepository): ILoginService
{
  public async Task<Usuario?> IniciarSesion(DTOIniciarSesion dto)
  {
    var usuario = await usuariosRepository.ObtenerUsuarioPorCorreo(dto.Correo);
    if (usuario != null && usuario.Activo)
    {
      using var hmac = new HMACSHA512(usuario.ContraseniaSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Contrasenia));
      for (var i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != usuario.ContraseniaHash[i]) return null;
      }
      return usuario;
    }
    return null;
  }
}
