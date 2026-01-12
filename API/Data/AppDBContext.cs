using Microsoft.EntityFrameworkCore;
using API.Entities;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
}