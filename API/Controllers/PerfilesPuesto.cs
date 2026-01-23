using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesPuesto(IPerfilesPuestoRepository perfilesPuestoRepository, IPerfilesPuestoService perfilesPuestoService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<PerfilPuesto>>> GetPerfilesPuesto()
        {
            var usuarios = await perfilesPuestoRepository.ObtenerPerfilesPuesto();
            return Ok(usuarios);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<PerfilPuesto>> GetPerfilPuesto([Required] int IDPerfilPuesto)
        {
            var usuario = await perfilesPuestoRepository.ObtenerPerfilPuesto(IDPerfilPuesto);
            return Ok(usuario);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<PerfilPuesto>> CrearPerfilPuesto([FromBody] DTOCrearPerfilPuesto dto)
        {
            bool res= await perfilesPuestoService.CrearPerfilPuesto(dto);
            if (res)
            {
                return Ok();
            }
            throw new Exception("Error al crear el registro");
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<PerfilPuesto>> ActualizarPerfilPuesto([FromBody] DTOActualizarPerfilPuesto dto)
        {
            bool res= await perfilesPuestoService.ActualizarPerfilPuesto(dto);
            if (res)
            {
                return Ok();
            }
            throw new Exception("Error al actualizar el registro");
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<PerfilPuesto>> InhabilitarPerfilPuesto([Required] int IDPerfilPuesto)
        {
            bool res= await perfilesPuestoRepository.InhabilitarPerfilPuesto(IDPerfilPuesto);
            if (res)
            {
                return Ok();
            }
            throw new Exception("Error al inhabilitar el registro");
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<PerfilPuesto>> HabilitarPerfilPuesto([Required] int IDPerfilPuesto)
        {
            bool res= await perfilesPuestoRepository.HabilitarPerfilPuesto(IDPerfilPuesto);
            if (res)
            {
                return Ok();
            }
            throw new Exception("Error al habilitar el registro");
        }
    }
}
