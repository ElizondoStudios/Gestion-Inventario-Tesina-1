using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class PerfilesPuestoService(IPerfilesPuestoRepository perfilesPuestoRepository): IPerfilesPuestoService
{
  private static DTOPerfilPuesto? ConvertirDTO(PerfilPuesto? registro)
  {
    return registro == null ?
    null :
    new DTOPerfilPuesto
    {
      IDPerfilPuesto = registro.IDPerfilPuesto,
      Descripcion = registro.Descripcion,
      Activo = registro.Activo
    };
  }

  public async Task<IReadOnlyList<DTOPerfilPuesto>> ObtenerPerfilesPuesto()
  {
    var registros = await perfilesPuestoRepository.ObtenerPerfilesPuesto();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }
  
  public async Task<IReadOnlyList<DTOPerfilPuesto>> ObtenerPerfilesPuestoActivos()
  {
    var registros = await perfilesPuestoRepository.ObtenerPerfilesPuesto();
    return [.. registros.Where(pp => pp.Activo).Select(r => ConvertirDTO(r)!)]; 
  }

  public async Task<DTOPerfilPuesto> ObtenerPerfilPuesto(int IDPerfilPuesto)
  {
    var registro = await perfilesPuestoRepository.ObtenerPerfilPuesto(IDPerfilPuesto) ?? throw new Exception("No se encontró el registro");
    return ConvertirDTO(registro)!;
  }

  public async Task<DTOPerfilPuesto> CrearPerfilPuesto(DTOCrearPerfilPuesto dto)
  {
    // Crear nuevo registro
    var registro = new PerfilPuesto
    {
      Descripcion = dto.Descripcion,
      Activo = true
    };

    // Persistir
    if(!await perfilesPuestoRepository.CrearPerfilPuesto(registro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    return ConvertirDTO(registro)!;
  }
  
  public async Task<DTOPerfilPuesto> ActualizarPerfilPuesto(DTOActualizarPerfilPuesto dto)
  {
    var registro = await perfilesPuestoRepository.ObtenerPerfilPuesto(dto.IDPerfilPuesto) ?? throw new Exception("Usuario no encontrado");
    
    // Validar activo
    if (!registro.Activo)
      throw new Exception("No se puede modificar un perfilpuesto inactivo");

    // Aplicar cambios
    registro.Descripcion = dto.Descripcion;

    // Persistir
    if(!await perfilesPuestoRepository.ActualizarPerfilPuesto(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el registro");
    }

    return ConvertirDTO(registro)!;
  }

  public async Task InhabilitarPerfilPuesto(int IDPerfilPuesto)
  {
    var success= await perfilesPuestoRepository.InhabilitarPerfilPuesto(IDPerfilPuesto);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
 
  public async Task HabilitarPerfilPuesto(int IDPerfilPuesto)
  {
    var success= await perfilesPuestoRepository.HabilitarPerfilPuesto(IDPerfilPuesto);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabiltiar el registro");
    }
    return;
  }
}
