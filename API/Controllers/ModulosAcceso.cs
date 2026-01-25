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
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOModulosAcceso>>> ObtenerModulosAcceso()
        {
            var res = await modulosAccesoService.ObtenerModulosAcceso();
            return Ok(res);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DTOModulosAcceso>> ValidarAccesoModulo(int IDUsuario, int IDModulo)
        {
            var res = await modulosAccesoService.ValidarAccesoModulo(IDUsuario, IDModulo);
            
            if (res == null)
            {
                return NotFound(new { mensaje = "No se encontró acceso para el usuario en el módulo especificado" });
            }
            
            return Ok(res);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DTOModulosAcceso>> RegistrarAccesoModulo([FromBody] DTORegistrarAccesoModulo dto)
        {
            var res = await modulosAccesoService.RegistrarAccesoModulo(dto);
            
            if (res == null)
            {
                return BadRequest(new { mensaje = "No se pudo registrar el acceso al módulo" });
            }
            
            return CreatedAtAction(nameof(ObtenerModulosAcceso), new { id = res.IDModuloAcceso }, res);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<DTOModulosAcceso>> EliminarAccesoModulo(int IDModuloAcceso)
        {
            var res = await modulosAccesoService.EliminarAccesoModulo(IDModuloAcceso);
            
            if (res == null)
            {
                return NotFound(new { mensaje = "No se encontró el acceso al módulo especificado" });
            }
            
            return Ok(res);
        }
    }
}
