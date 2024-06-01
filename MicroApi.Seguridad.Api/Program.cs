using MicroApi.Seguridad.Api.Utilidades;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
            Title = "ChairaAPI",
            Version = "v1",
            Description = "Web Api para OTI de la Universidad de la Amazonia (Florencia - Caquetá)",
            Contact = new OpenApiContact
            {
                Email = "german.andres201@gmail.com",
                Name = "German Ortiz"
            },
            License = new OpenApiLicense
            {
                Name = "Derechos Reservados",
            }
        });
        c.SwaggerDoc("v2", new OpenApiInfo
        {
            Title = "ChairaAPI",
            Version = "v2",
            Description = "Web Api para OTI de la Universidad de la Amazonia (Florencia - Caquetá)",
            Contact = new OpenApiContact
            {
                Email = "german.andres201@gmail.com",
                Name = "German Ortiz"
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

builder.Services.AddDbContext<ModelContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("oracleConnection")));

builder.Services.AddScoped<IUtilitiesService, UtilitiesService>();
builder.Services.AddScoped<IUtilitiesRepository, UtilitiesRepository>();

builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChairaAPI v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "ChairaAPI v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
