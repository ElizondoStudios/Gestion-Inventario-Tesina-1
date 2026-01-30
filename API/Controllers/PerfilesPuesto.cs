using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesPuesto(IPerfilesPuestoService perfilesPuestoService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOPerfilPuesto>>> GetPerfilesPuesto()
        {
            var res = await perfilesPuestoService.ObtenerPerfilesPuesto();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOPerfilPuesto>> GetPerfilPuesto([Required] int IDPerfilPuesto)
        {
            var res = await perfilesPuestoService.ObtenerPerfilPuesto(IDPerfilPuesto);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOPerfilPuesto>> CrearPerfilPuesto([FromBody] DTOCrearPerfilPuesto dto)
        {
            var res= await perfilesPuestoService.CrearPerfilPuesto(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOPerfilPuesto>> ActualizarPerfilPuesto([FromBody] DTOActualizarPerfilPuesto dto)
        {
            var res= await perfilesPuestoService.ActualizarPerfilPuesto(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOPerfilPuesto>> InhabilitarPerfilPuesto([Required] int IDPerfilPuesto)
        {
            await perfilesPuestoService.InhabilitarPerfilPuesto(IDPerfilPuesto);
            return Ok();
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOPerfilPuesto>> HabilitarPerfilPuesto([Required] int IDPerfilPuesto)
        {
            await perfilesPuestoService.HabilitarPerfilPuesto(IDPerfilPuesto);
            return Ok();
        }
    }
}
