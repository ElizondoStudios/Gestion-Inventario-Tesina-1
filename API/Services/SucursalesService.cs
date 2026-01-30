using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class SucursalesService(ISucursalRepository sucursalRepository): ISucursalesService
{
  private static DTOSucursal? ConvertirDTO(Sucursal? registro)
  {
    return registro == null ?
    null :
    new DTOSucursal
    {
      IDSucursal = registro.IDSucursal,
      Nombre = registro.Nombre,
      Direccion = registro.Direccion,
      Activo = registro.Activo
    };
  }

  public async Task<IReadOnlyList<DTOSucursal>> ObtenerSucursales()
  {
    var registros = await sucursalRepository.ObtenerSucursales();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }

  public async Task<DTOSucursal> ObtenerSucursal(int IDSucursal)
  {
    var registro = await sucursalRepository.ObtenerSucursal(IDSucursal) ?? throw new Exception("No se encontró el registro");
    return ConvertirDTO(registro)!;
  }

  public async Task<DTOSucursal> CrearSucursal(DTOCrearSucursal dto)
  {
    // Crear nuevo registro
    var registro = new Sucursal
    {
      Nombre = dto.Nombre,
      Direccion = dto.Direccion,
      Activo = true
    };

    // Persistir
    if(!await sucursalRepository.CrearSucursal(registro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    return ConvertirDTO(registro)!;
  }
  
  public async Task<DTOSucursal> ActualizarSucursal(DTOActualizarSucursal dto)
  {
    var registro = await sucursalRepository.ObtenerSucursal(dto.IDSucursal) ?? throw new Exception("Usuario no encontrado");
    
    // Validar activo
    if (!registro.Activo)
      throw new Exception("No se puede modificar un perfilpuesto inactivo");

    // Aplicar cambios
    registro.Nombre = dto.Nombre;
    registro.Direccion = dto.Direccion;

    // Persistir
    if(!await sucursalRepository.ActualizarSucursal(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el registro");
    }

    return ConvertirDTO(registro)!;
  }

  public async Task InhabilitarSucursal(int IDSucursal)
  {
    var success= await sucursalRepository.InhabilitarSucursal(IDSucursal);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
 
  public async Task HabilitarSucursal(int IDSucursal)
  {
    var success= await sucursalRepository.HabilitarSucursal(IDSucursal);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
}
