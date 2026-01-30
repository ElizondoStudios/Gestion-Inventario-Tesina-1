using System;
using API.Data.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface ILoginService
{
  Task<Usuario?> IniciarSesion(DTOIniciarSesion dto);
}
