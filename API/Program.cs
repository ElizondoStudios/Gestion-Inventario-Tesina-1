using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Documentación del API
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        // Agregar controladores
        builder.Services.AddControllers();
        
        // Agregar DB Context
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
        });

        // Inyección de repositorios
        InyectarRepositorios(builder);
        
        // Injección de los servicios
        InyectarServicios(builder);

        var app = builder.Build();

        // Hacer una migración de DB
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Migration process failed!");
        }

        // Si el entorno es de desarrollo empezamos Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();

    }

    public static void InyectarRepositorios(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
        builder.Services.AddScoped<IPerfilesPuestoRepository, PerfilesPuestoRepository>();
    }
    
    public static void InyectarServicios(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsuariosService, UsuarioService>();
        builder.Services.AddScoped<IPerfilesPuestoService, PerfilesPuestoService>();
    }
}

