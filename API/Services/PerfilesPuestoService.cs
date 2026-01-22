using System;
using API.Data.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class PerfilesPuestoService(IPerfilesPuestoRepository perfilesPuestoRepository): IPerfilesPuestoService
{
  public async Task<bool> CrearPerfilPuesto(DTOCrearPerfilPuesto dto)
  {
    var perfilpuesto = new PerfilPuesto
    {
      Descripcion = dto.Descripcion,
      Activo = true,
    };

    // Persistir
    return await perfilesPuestoRepository.CrearPerfilPuesto(perfilpuesto);
  }
  
  public async Task<bool> ActualizarPerfilPuesto(DTOActualizarPerfilPuesto dto)
  {
    var perfilpuesto = await perfilesPuestoRepository.ObtenerPerfilPuesto(dto.IDPerfilPuesto) ?? throw new Exception("Usuario no encontrado");
    
    // Validar activo
    if (!perfilpuesto.Activo)
      throw new Exception("No se puede modificar un perfilpuesto inactivo");

    // Aplicar cambios
    perfilpuesto.Descripcion = dto.Descripcion;

    // Persistir
    return await perfilesPuestoRepository.ActualizarPerfilPuesto(perfilpuesto);
  }
}
