using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Data.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Interfaces;
using API.Services;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios(IUsuariosRepository usuariosRepository, IUsuariosService usuariosService) : ControllerBase
    {   
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<Usuario>>> GetUsuarios()
        {
            var usuarios = await usuariosRepository.ObtenerUsuarios();
            return Ok(usuarios);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<Usuario>> GetUsuario(int IDUsuario)
        {
            var usuario = await usuariosRepository.ObtenerUsuario(IDUsuario);
            return Ok(usuario);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] DTOCrearUsuario dto)
        {
            bool res= await usuariosService.CrearUsuario(dto);
            if (res)
            {
                return Ok();
            }
            throw new Exception("Error al crear el registro");
        }
    }
}
