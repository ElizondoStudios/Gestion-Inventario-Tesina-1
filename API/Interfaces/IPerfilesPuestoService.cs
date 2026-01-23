using System;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IPerfilesPuestoService
{
    Task<IReadOnlyList<DTOPerfilPuesto>> ObtenerPerfilesPuesto();
    Task<DTOPerfilPuesto> ObtenerPerfilPuesto(int IDPerfilPuesto);
    Task<DTOPerfilPuesto> CrearPerfilPuesto(DTOCrearPerfilPuesto dto);
    Task<DTOPerfilPuesto> ActualizarPerfilPuesto(DTOActualizarPerfilPuesto dto);
    Task InhabilitarPerfilPuesto(int IDPerfilPuesto);
    Task HabilitarPerfilPuesto(int IDPerfilPuesto);
}
