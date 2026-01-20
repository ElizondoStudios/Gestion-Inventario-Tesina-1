using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using API.Entities;
using API.Repositories;
using API.Data.DTOs;

namespace API.Services;

public class UsuarioService(IUsuariosRepository usuariosRepository): IUsuariosService
{
  public async Task<bool> CrearUsuario(DTOCrearUsuario dto)
  {
    // Hashear contrasenia
    using var hmac = new HMACSHA512();
    byte[] contraseniaSalt = hmac.Key;
    byte[] contraseniaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Contrasenia));

    // Crear nuevo registro
    var usuario = new Usuario
    {
      Nombre = dto.Nombre,
      Correo = dto.Correo,
      ContraseniaHash = contraseniaHash,
      ContraseniaSalt = contraseniaSalt,
      Activo = true,
      IDPerfilPuesto = dto.IDPerfilPuesto
    };

    // Persistir
    return await usuariosRepository.CrearUsuario(usuario);
  }
  
  public async Task<bool> ActualizarUsuario(DTOActualizarUsuario dto)
  {
    var usuario = await usuariosRepository.ObtenerUsuario(dto.IDUsuario) ?? throw new Exception("Usuario no encontrado");
    
    // Validar activo
    if (!usuario.Activo)
      throw new Exception("No se puede modificar un usuario inactivo");

    // Validar correo único
    if (await usuariosRepository.ExisteCorreo(dto.Correo, dto.IDUsuario))
      throw new Exception("El correo ya está en uso");

    // Aplicar cambios
    usuario.Nombre = dto.Nombre;
    usuario.Correo = dto.Correo;
    usuario.IDPerfilPuesto = dto.IDPerfilPuesto;

    // Persistir
    return await usuariosRepository.ActualizarUsuario(usuario);
  }
}