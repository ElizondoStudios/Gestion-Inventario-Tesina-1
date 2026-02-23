using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class UsuarioSucursalService(IUsuarioSucursalRepository usuarioSucursalRepository): IUsuarioSucursalService
{
  private static DTOUsuarioSucursal? ConvertirDTO(UsuarioSucursal? registro)
  {
    return registro == null ?
    null :
    new DTOUsuarioSucursal
    {
      IDSucursalUsuario = registro.IDSucursalUsuario,
      Activo = registro.Activo,
      IDUsuario = registro.IDUsuario,
      NombreUsuario = registro.Usuario.Nombre,
      IDSucursal = registro.IDSucursal,
      NombreSucursal = registro.Sucursal.Nombre
    };
  }

  public async Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerUsuariosSucursales()
  {
    var registros = await usuarioSucursalRepository.ObtenerUsuariosSucursales();
    return [.. registros.Select(r => ConvertirDTO(r)!)];
  }

  public async Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerSucursalesPorUsuario(int IDUsuario)
  {
    var registros = await usuarioSucursalRepository.ObtenerSucursalesPorUsuario(IDUsuario);
    return [.. registros.Select(r => ConvertirDTO(r)!)];
  }

  public async Task<IReadOnlyList<DTOUsuarioSucursal>> ObtenerUsuariosPorSucursal(int IDSucursal)
  {
    var registros = await usuarioSucursalRepository.ObtenerUsuariosPorSucursal(IDSucursal);
    return [.. registros.Select(r => ConvertirDTO(r)!)];
  }

  public async Task<DTOUsuarioSucursal?> ObtenerUsuarioSucursal(int IDSucursalUsuario)
  {
    var registro = await usuarioSucursalRepository.ObtenerUsuarioSucursal(IDSucursalUsuario);
    return ConvertirDTO(registro);
  }

  public async Task<DTOUsuarioSucursal?> CrearUsuarioSucursal(DTOCrearUsuarioSucursal dto)
  {
    var nuevoRegistro = new UsuarioSucursal
    {
      Activo = true,
      IDUsuario = dto.IDUsuario,
      IDSucursal = dto.IDSucursal
    };

    if (!await usuarioSucursalRepository.CrearUsuarioSucursal(nuevoRegistro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    var registroCreado = await usuarioSucursalRepository.ObtenerUsuarioSucursal(nuevoRegistro.IDSucursalUsuario);
    return ConvertirDTO(registroCreado);
  }

  public async Task<DTOUsuarioSucursal?> InhabilitarUsuarioSucursal(int IDSucursalUsuario)
  {
    var success = await usuarioSucursalRepository.InhabilitarUsuarioSucursal(IDSucursalUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabilitar el registro");
    }

    var registro = await usuarioSucursalRepository.ObtenerUsuarioSucursal(IDSucursalUsuario);
    return ConvertirDTO(registro);
  }
  
  public async Task<bool> EliminarUsuarioSucursal(int IDSucursalUsuario)
  {
    var success = await usuarioSucursalRepository.EliminarUsuarioSucursal(IDSucursalUsuario);
    return success;
  }

  public async Task<DTOUsuarioSucursal?> HabilitarUsuarioSucursal(int IDSucursalUsuario)
  {
    var success = await usuarioSucursalRepository.HabilitarUsuarioSucursal(IDSucursalUsuario);
    if (!success)
    {
      throw new Exception("Hubo un error al habilitar el registro");
    }

    var registro = await usuarioSucursalRepository.ObtenerUsuarioSucursal(IDSucursalUsuario);
    return ConvertirDTO(registro);
  }
}
