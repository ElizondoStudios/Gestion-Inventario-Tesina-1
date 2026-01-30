using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

        // Agregar auth
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = builder.Configuration["TokenKey"]
                ?? throw new ArgumentNullException("Cannot get the token key - Program.cs");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
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

        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

    public static void InyectarRepositorios(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
        builder.Services.AddScoped<IPerfilesPuestoRepository, PerfilesPuestoRepository>();
        builder.Services.AddScoped<IModulosAccesoRepository, ModulosAccesoRepository>();
        builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
        builder.Services.AddScoped<IUnidadRepository, UnidadRepository>();
        builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
        builder.Services.AddScoped<ILogInventarioRepository, LogInventarioRepository>();
        builder.Services.AddScoped<IUsuarioSucursalRepository, UsuarioSucursalRepository>();
        builder.Services.AddScoped<ISucursalesInventarioRepository, SucursalesInventarioRepository>();
    }
    
    public static void InyectarServicios(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsuariosService, UsuarioService>();
        builder.Services.AddScoped<IPerfilesPuestoService, PerfilesPuestoService>();
        builder.Services.AddScoped<ISucursalesService, SucursalesService>();
        builder.Services.AddScoped<IUnidadesService, UnidadesService>();
        builder.Services.AddScoped<IInventarioService, InventarioService>();
        builder.Services.AddScoped<ILogInventarioService, LogInventarioService>();
        builder.Services.AddScoped<IUsuarioSucursalService, UsuarioSucursalService>();
        builder.Services.AddScoped<ISucursalesInventarioService, SucursalesInventarioService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
    }
}

