using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IPerfilesPuestoService
{
    Task<bool> CrearPerfilPuesto(DTOCrearPerfilPuesto dto);
    Task<bool> ActualizarPerfilPuesto(DTOActualizarPerfilPuesto dto);
}
