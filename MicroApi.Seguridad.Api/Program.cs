using MicroApi.Seguridad.Api.Utilidades;
using MicroApi.Seguridad.Api;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Data.Repository;
using MicroApi.Seguridad.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Cors;
using MicroApi.Seguridad.Data.Conections;
using MicroApi.Seguridad.Data.Utilities;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers( opc =>
{
    opc.Conventions.Add(new SwaggerAgruparVersion());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "EndPoints de Incidencias",
            Version = "Final",
            Description = "Funcionamiento de todo el proceso de gestión de incidencias para la mesa de servicios",
            Contact = new OpenApiContact
            {
                Email = "joh.mosquera@udla.edu.co",
                Name = "Johan Sebastián Mosquera Munar"
            },
            License = new OpenApiLicense
            {
                Name = "Derechos Reservados",
            }
        });

        c.SwaggerDoc("v2", new OpenApiInfo
        {
            Title = "EndPoints de Roles",
            Version = "VersionFinal",
            Description = "Versi�n final de los roles en la API para la OTI de la UDLA para pruebas de la mesa de servicios",
            Contact = new OpenApiContact
            {
                Email = "joh.mosquera@udla.edu.co",
                Name = "Johan Sebasti�n Mosquera Munar"
            },
            License = new OpenApiLicense
            {
                Name = "Derechos Reservados",
            }
        });

        c.SwaggerDoc("v4", new OpenApiInfo
        {
            Title = "EndPoints de Inventario",
            Version = "VersionFinal",
            Description = "Versi�n final del inventario en la API para la OTI de la UDLA para pruebas de la mesa de servicios",
            Contact = new OpenApiContact
            {
                Email = "joh.mosquera@udla.edu.co",
                Name = "Johan Sebasti�n Mosquera Munar"
            },
            License = new OpenApiLicense
            {
                Name = "Derechos Reservados",
            }
        });

        c.SwaggerDoc("v5", new OpenApiInfo
        {
            Title = "EndPoints de Incidencias",
            Version = "VersionFinal",
            Description = "Versi�n final de las incidencias en la API para la OTI de la UDLA para pruebas de la mesa de servicios",
            Contact = new OpenApiContact
            {
                Email = "joh.mosquera@udla.edu.co",
                Name = "Johan Sebasti�n Mosquera Munar"
            },
            License = new OpenApiLicense
            {
                Name = "Derechos Reservados",
            }
        });


        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        //Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("passwordToken"))),
                        Encoding.UTF8.GetBytes("xxxxxxxxxxxxxxxxxxxxx")),
                    ClockSkew = TimeSpan.FromSeconds(1)
                });

builder.Services.AddDbContext<ModelContextORACLE>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("oracleConnection")));

builder.Services.AddDbContext<ModelContextSQL>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("sqlServerConnection")));

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("mongoConnection"));
builder.Services.AddSingleton<MongoConnection>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SMTP"));

builder.Services.AddScoped<IUtilitiesService, UtilitiesService>();
builder.Services.AddScoped<IUtilitiesRepository, UtilitiesRepository>();

builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();

builder.Services.AddScoped<IOracleRepository, OracleRepository>();
builder.Services.AddScoped<IOracleService, OracleService>();

builder.Services.AddScoped<IIncidenciaRepository, IncidenciaRepository>();
builder.Services.AddScoped<IIncidenciaService, IncidenciaService>();

builder.Services.AddScoped<ITrazabilidadRepository, TrazabilidadRepository>();
builder.Services.AddScoped<ITrazabilidadService, TrazabilidadService>();

builder.Services.AddScoped<ISeguimientoRepository, SeguimientoRepository>();
builder.Services.AddScoped<ISeguimientoService, SeguimientoService>();

builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<IHistoricoService, HistoricoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<IInventarioService, InventarioService>();

builder.Services.AddScoped<IEvidenciaRepository, EvidenciaRepository>();
builder.Services.AddScoped<IEvidenciaService, EvidenciaService>();

builder.Services.AddScoped<IEnviarCorreoRepository, EnviarCorreoRepository>();
builder.Services.AddScoped<IEnviarCorreoService, EnviarCorreoService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction() || !app.Environment.IsDevelopment())//Development para MacOs, Production para Windows
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Incidencia");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "NO-Roles");
        c.SwaggerEndpoint("/swagger/v4/swagger.json", "No-Inventario");
        c.SwaggerEndpoint("/swagger/v5/swagger.json", "NO-Incidencias");
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
