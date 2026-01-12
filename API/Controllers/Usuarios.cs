using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Data.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios(AppDbContext context) : ControllerBase
    {   
        [HttpGet("[action]")]
        public async Task<ActionResult<IReadOnlyList<Usuario>>> GetUsuarios()
        {
            var usuarios = await context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }
    }
}
