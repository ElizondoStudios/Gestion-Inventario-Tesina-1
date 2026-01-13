using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Data.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Interfaces;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios(IUsuariosRepository usuariosRepository) : ControllerBase
    {   
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<Usuario>>> GetUsuarios()
        {
            var usuarios = await usuariosRepository.ObtenerUsuarios();
            return Ok(usuarios);
        }
    }
}
