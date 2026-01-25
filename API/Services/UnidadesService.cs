using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class UnidadesService(IUnidadRepository unidadRepository): IUnidadesService
{
  private static DTOUnidad? ConvertirDTO(Unidad? registro)
  {
    return registro == null ?
    null :
    new DTOUnidad
    {
      IDUnidad = registro.IDUnidad,
      Descripcion = registro.Descripcion,
      Abreviacion = registro.Abreviacion,
      Activo = registro.Activo
    };
  }

  public async Task<IReadOnlyList<DTOUnidad>> ObtenerUnidades()
  {
    var registros = await unidadRepository.ObtenerUnidades();
    return [.. registros.Select(r => ConvertirDTO(r)!)]; 
  }

  public async Task<DTOUnidad> ObtenerUnidad(int IDUnidad)
  {
    var registro = await unidadRepository.ObtenerUnidad(IDUnidad) ?? throw new Exception("No se encontró el registro");
    return ConvertirDTO(registro)!;
  }

  public async Task<DTOUnidad> CrearUnidad(DTOCrearUnidad dto)
  {
    // Crear nuevo registro
    var registro = new Unidad
    {
      Descripcion = dto.Descripcion,
      Abreviacion = dto.Abreviacion,
      Activo = true
    };

    // Persistir
    if(!await unidadRepository.CrearUnidad(registro))
    {
      throw new Exception("Ocurrió un error al crear el registro");
    }

    return ConvertirDTO(registro)!;
  }
  
  public async Task<DTOUnidad> ActualizarUnidad(DTOActualizarUnidad dto)
  {
    var registro = await unidadRepository.ObtenerUnidad(dto.IDUnidad) ?? throw new Exception("Unidad no encontrada");
    
    // Validar activo
    if (!registro.Activo)
      throw new Exception("No se puede modificar una unidad inactiva");

    // Aplicar cambios
    registro.Descripcion = dto.Descripcion;
    registro.Abreviacion = dto.Abreviacion;

    // Persistir
    if(!await unidadRepository.ActualizarUnidad(registro))
    {
      throw new Exception("Ocurrió un error al actualizar el registro");
    }

    return ConvertirDTO(registro)!;
  }

  public async Task InhabilitarUnidad(int IDUnidad)
  {
    var success= await unidadRepository.InhabilitarUnidad(IDUnidad);
    if (!success)
    {
      throw new Exception("Hubo un error al inhabilitar el registro");
    }
    return;
  }
 
  public async Task HabilitarUnidad(int IDUnidad)
  {
    var success= await unidadRepository.HabilitarUnidad(IDUnidad);
    if (!success)
    {
      throw new Exception("Hubo un error al habilitar el registro");
    }
    return;
  }
}
