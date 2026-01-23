using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using API.Entities;
using API.Repositories;
using API.Data.DTOs;

namespace API.Services;

public class UsuarioService(IUsuariosRepository usuariosRepository): IUsuariosService
{
  public async Task<IReadOnlyList<DTOUsuario>> ObtenerUsuarios()
  {
    var usuarios = await usuariosRepository.ObtenerUsuarios();
    return [.. usuarios.Select(u => new DTOUsuario
    {
      IDUsuario = u.IDUsuario,
      Nombre = u.Nombre,
      Correo = u.Correo,
      IDPerfilPuesto = u.IDPerfilPuesto,
      Activo = u.Activo
    })]; 
  }

  public async Task<DTOUsuario> ObtenerUsuario(int IDUsuario)
  {
    var usuario = await usuariosRepository.ObtenerUsuario(IDUsuario) ?? throw new Exception("No se encontró el usuario");
    return new DTOUsuario
    {
      IDUsuario = usuario.IDUsuario,
      Nombre = usuario.Nombre,
      Correo = usuario.Correo,
      IDPerfilPuesto = usuario.IDPerfilPuesto,
      Activo = usuario.Activo
    };
  }

  public async Task<DTOUsuario> CrearUsuario(DTOCrearUsuario dto)
  {
    // Validar correo único
    if (await usuariosRepository.ExisteCorreo(dto.Correo, 0))
      throw new Exception("El correo ya está en uso");

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
    if(!await usuariosRepository.CrearUsuario(usuario))
    {
      throw new Exception("Ocurrió un error al crear el usuario");
    }

    return new DTOUsuario
    {
      IDUsuario = usuario.IDUsuario,
      Nombre = usuario.Nombre,
      Correo = usuario.Correo,
      Activo = usuario.Activo,
      IDPerfilPuesto = usuario.IDPerfilPuesto
    };
  }
  
  public async Task<DTOUsuario> ActualizarUsuario(DTOActualizarUsuario dto)
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
    if(!await usuariosRepository.ActualizarUsuario(usuario))
    {
      throw new Exception("Ocurrió un error al actualizar el usuario");
    }

    return new DTOUsuario
    {
      IDUsuario = usuario.IDUsuario,
      Nombre = usuario.Nombre,
      Correo = usuario.Correo,
      Activo = usuario.Activo,
      IDPerfilPuesto = usuario.IDPerfilPuesto
    };
  }

  public async Task InhabilitarUsuario(int IDUsuario)
  {
    var success= await usuariosRepository.InhabilitarUsuario(IDUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el usuario");
    }
    return;
  }
 
  public async Task HabilitarUsuario(int IDUsuario)
  {
    var success= await usuariosRepository.HabilitarUsuario(IDUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el usuario");
    }
    return;
  }
}