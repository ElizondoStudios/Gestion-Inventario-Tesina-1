using System.ComponentModel.DataAnnotations;
using API.Data.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Unidades(IUnidadesService unidadesService) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOUnidad>>> GetUnidades()
        {
            var res = await unidadesService.ObtenerUnidades();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOUnidad>> GetUnidad([Required] int IDUnidad)
        {
            var res = await unidadesService.ObtenerUnidad(IDUnidad);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOUnidad>> CrearUnidad([FromBody] DTOCrearUnidad dto)
        {
            var res= await unidadesService.CrearUnidad(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUnidad>> ActualizarUnidad([FromBody] DTOActualizarUnidad dto)
        {
            var res= await unidadesService.ActualizarUnidad(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUnidad>> InhabilitarUnidad([Required] int IDUnidad)
        {
            await unidadesService.InhabilitarUnidad(IDUnidad);
            return Ok();
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUnidad>> HabilitarUnidad([Required] int IDUnidad)
        {
            await unidadesService.HabilitarUnidad(IDUnidad);
            return Ok();
        }
    }
}
