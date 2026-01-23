using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Data.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Interfaces;
using API.Services;
using API.Data.DTOs;
using System.ComponentModel.DataAnnotations;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios(IUsuariosService usuariosService) : ControllerBase
    {   
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<DTOUsuario>>> GetUsuarios()
        {
            var res = await usuariosService.ObtenerUsuarios();
            return Ok(res);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<DTOUsuario>> GetUsuario([Required] int IDUsuario)
        {
            var res = await usuariosService.ObtenerUsuario(IDUsuario);
            return Ok(res);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<DTOUsuario>> CrearUsuario([FromBody] DTOCrearUsuario dto)
        {
            var res= await usuariosService.CrearUsuario(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult<DTOUsuario>> ActualizarUsuario([FromBody] DTOActualizarUsuario dto)
        {
            var res= await usuariosService.ActualizarUsuario(dto);
            return Ok(res);
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult> InhabilitarUsuario([Required] int IDUsuario)
        {
            await usuariosService.InhabilitarUsuario(IDUsuario);
            return Ok();
        }
        
        [HttpPut("[action]")]
        public async Task<ActionResult> HabilitarUsuario([Required] int IDUsuario)
        {
            await usuariosService.HabilitarUsuario(IDUsuario);
            return Ok();
        }
    }
}
