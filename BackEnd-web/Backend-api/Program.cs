using Backend.Api.Configurations;
using Backend.Api.Data; // Asegúrate que el nombre de tu DbContext esté aquí
using Backend.Api.Repositories;
using Backend.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. BASE DE DATOS (PostgreSQL) ---
// Lee la cadena de conexión desde las variables de entorno de Render.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- 2. INYECCIÓN DE DEPENDENCIAS (Repositorios y Servicios) ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
builder.Services.AddScoped<ArticuloService>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<PermisoService>();

// --- 3. CONFIGURACIÓN DE JWT (Token) ---
// Se configura para leer la clave secreta de forma segura.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // FIX: Se obtiene el secreto directamente de la configuración para evitar errores de null.
    // Render inyectará este valor desde las variables de entorno.
    var jwtSecret = builder.Configuration["JwtConfig:Secret"];
    if (string.IsNullOrEmpty(jwtSecret))
    {
        // Lanza un error claro si la clave no está configurada.
        throw new InvalidOperationException("El secreto de JWT (JwtConfig:Secret) no está configurado.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // No permite desfase de tiempo.
    };
});

// --- 4. SWAGGER Y CONTROLADORES ---
// Se mantiene tu configuración para evitar ciclos en las respuestas JSON.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
// Asumo que AddSwaggerWithJwt() es un método de extensión que ya tienes configurado.
// Si no, reemplaza la siguiente línea por: builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(); // Usando el AddSwaggerGen estándar para asegurar compatibilidad.

// --- 5. CORS ---
// Permite que tu frontend (u otras apps) puedan hacerle peticiones a tu API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// --- 6. APLICAR MIGRACIONES AUTOMÁTICAMENTE ---
// Esto es ideal para producción. La base de datos se crea o actualiza sola al iniciar la app.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Aplicando migraciones de la base de datos...");
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        logger.LogInformation("Migraciones aplicadas correctamente.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones de la base de datos.");
    }
}

// --- 7. PIPELINE DE MIDDLEWARES ---
// Define el orden en que se procesan las peticiones HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(); // Es importante que vaya antes de Authentication y Authorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
