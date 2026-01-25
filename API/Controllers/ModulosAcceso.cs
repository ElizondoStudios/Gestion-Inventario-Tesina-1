using API.Data.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosAcceso(IModulosAccesoService modulosAccesoService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DTOModulosAcceso>>> ObtenerModulosAcceso()
        {
            var registros = await modulosAccesoService.ObtenerModulosAcceso();
            return Ok(registros);
        }

        [HttpGet("validar/{IDUsuario}/{IDModulo}")]
        public async Task<ActionResult<DTOModulosAcceso>> ValidarAccesoModulo(int IDUsuario, int IDModulo)
        {
            var registro = await modulosAccesoService.ValidarAccesoModulo(IDUsuario, IDModulo);
            
            if (registro == null)
            {
                return NotFound(new { mensaje = "No se encontró acceso para el usuario en el módulo especificado" });
            }
            
            return Ok(registro);
        }

        [HttpPost]
        public async Task<ActionResult<DTOModulosAcceso>> RegistrarAccesoModulo([FromBody] DTORegistrarAccesoModulo dto)
        {
            var registro = await modulosAccesoService.RegistrarAccesoModulo(dto);
            
            if (registro == null)
            {
                return BadRequest(new { mensaje = "No se pudo registrar el acceso al módulo" });
            }
            
            return CreatedAtAction(nameof(ObtenerModulosAcceso), new { id = registro.IDModuloAcceso }, registro);
        }

        [HttpDelete("{IDModuloAcceso}")]
        public async Task<ActionResult<DTOModulosAcceso>> EliminarAccesoModulo(int IDModuloAcceso)
        {
            var registro = await modulosAccesoService.EliminarAccesoModulo(IDModuloAcceso);
            
            if (registro == null)
            {
                return NotFound(new { mensaje = "No se encontró el acceso al módulo especificado" });
            }
            
            return Ok(registro);
        }
    }
}
