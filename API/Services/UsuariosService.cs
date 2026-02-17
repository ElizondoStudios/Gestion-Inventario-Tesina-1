using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using API.Entities;
using API.Repositories;
using API.Data.DTOs;

namespace API.Services;

public class UsuarioService(IUsuariosRepository usuariosRepository): IUsuariosService
{
  private static DTOUsuario? ConvertirDTO(Usuario? registro)
  {
    return registro == null ?
    null :
    new DTOUsuario
    {
      IDUsuario = registro.IDUsuario,
      Nombre = registro.Nombre,
      Correo = registro.Correo,
      IDPerfilPuesto = registro.IDPerfilPuesto,
      DescripcionPerfilPuesto = registro.PerfilPuesto.Descripcion,
      Activo = registro.Activo
    };
  }

  public async Task<IReadOnlyList<DTOUsuario>> ObtenerUsuarios()
  {
    var registros = await usuariosRepository.ObtenerUsuarios();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }

  public async Task<DTOUsuario> ObtenerUsuario(int IDUsuario)
  {
    var registro = await usuariosRepository.ObtenerUsuario(IDUsuario) ?? throw new Exception("No se encontró el registro");
    return ConvertirDTO(registro)!;
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
    var registro = new Usuario
    {
      Nombre = dto.Nombre,
      Correo = dto.Correo,
      ContraseniaHash = contraseniaHash,
      ContraseniaSalt = contraseniaSalt,
      Activo = true,
      IDPerfilPuesto = dto.IDPerfilPuesto
    };

    // Persistir
    if(!await usuariosRepository.CrearUsuario(registro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    return ConvertirDTO(registro)!;
  }
  
  public async Task<DTOUsuario> ActualizarUsuario(DTOActualizarUsuario dto)
  {
    var registro = await usuariosRepository.ObtenerUsuario(dto.IDUsuario) ?? throw new Exception("Usuario no encontrado");
    
    // Validar activo
    if (!registro.Activo)
      throw new Exception("No se puede modificar un registro inactivo");

    // Validar correo único
    if (await usuariosRepository.ExisteCorreo(dto.Correo, dto.IDUsuario))
      throw new Exception("El correo ya está en uso");

    // Aplicar cambios
    registro.Nombre = dto.Nombre;
    registro.Correo = dto.Correo;
    registro.IDPerfilPuesto = dto.IDPerfilPuesto;

    // Persistir
    if(!await usuariosRepository.ActualizarUsuario(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el registro");
    }

    return ConvertirDTO(registro)!;
  }

  public async Task InhabilitarUsuario(int IDUsuario)
  {
    var success= await usuariosRepository.InhabilitarUsuario(IDUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
 
  public async Task HabilitarUsuario(int IDUsuario)
  {
    var success= await usuariosRepository.HabilitarUsuario(IDUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
}