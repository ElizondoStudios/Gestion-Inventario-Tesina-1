using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using API.Entities;
using API.Repositories;

namespace API.Services;

public class UsuarioService(IUsuariosRepository usuariosRepository): IUsuariosService
{
  public Task<bool> CrearUsuario(DTOCrearUsuario dto)
  {
    // Hashear contrasenia
    using var hmac = new HMACSHA512();
    byte[] contraseniaSalt = hmac.Key;
    byte[] contraseniaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Contrasenia));

    var usuario = new Usuario
    {
      Nombre = dto.Nombre,
      Correo = dto.Correo,
      ContraseniaHash = contraseniaHash,
      ContraseniaSalt = contraseniaSalt,
      Activo = true,
      PerfilPuesto = new PerfilPuesto
        {
          IDPerfilPuesto = dto.IDPerfilPuesto,
          Descripcion = string.Empty
        }
    };

    return usuariosRepository.CrearUsuario(usuario);
  }
}