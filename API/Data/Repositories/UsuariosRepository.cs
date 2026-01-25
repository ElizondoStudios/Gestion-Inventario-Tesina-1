using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Repositories;

public class UsuariosRepository(AppDbContext context): IUsuariosRepository 
{
  public async Task<IReadOnlyList<Usuario>> ObtenerUsuarios()
  {
    return await context.Usuarios
      .Include(u => u.PerfilPuesto)
      .Where(u => u.PerfilPuesto.Activo)
      .ToListAsync();
  }

  public async Task<Usuario?> ObtenerUsuario(int IDUsuario)
  {
    return await context.Usuarios
      .Include(u => u.PerfilPuesto)
      .Where(u => u.PerfilPuesto.Activo)
      .FirstOrDefaultAsync(u => u.IDUsuario == IDUsuario);
  }

  public async Task<bool> CrearUsuario(Usuario usuario)
  {
    await context.Usuarios.AddAsync(usuario);
    return await context.SaveChangesAsync() > 0;
  }

  public async Task<bool> ActualizarUsuario(Usuario usuario)
  {
    var filas = await context.Usuarios
    .Where(u => u.IDUsuario == usuario.IDUsuario)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Nombre, usuario.Nombre)
      .SetProperty(u => u.Correo, usuario.Correo)
      .SetProperty(u => u.IDPerfilPuesto, usuario.IDPerfilPuesto)
    );

    return filas > 0;
  }

  public async Task<bool> InhabilitarUsuario(int IDUsuario)
  {
    var filas = await context.Usuarios
    .Where(u => u.IDUsuario == IDUsuario)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Activo, false)
    );

    return filas > 0;
  }
  
  public async Task<bool> HabilitarUsuario(int IDUsuario)
  {
    var filas = await context.Usuarios
    .Where(u => u.IDUsuario == IDUsuario)
    .ExecuteUpdateAsync(setters => setters
      .SetProperty(u => u.Activo, true)
    );

    return filas > 0;
  }

  public async Task<bool> ExisteCorreo(string Correo, int IDUsuario)
  {
    var filas = context.Usuarios
      .Where(u => u.IDUsuario != IDUsuario)
      .Where(u => u.Correo == Correo);

    return filas.Any();
  }
}