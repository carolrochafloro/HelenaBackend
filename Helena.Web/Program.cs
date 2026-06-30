using Domain.Business;
using Domain.Entities;
using Domain.Interfaces.Business;
using Domain.Interfaces.Data;
using Helena.Web.Data.Context;
using Infra.Data.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://www.helenamed.com.br", "https://helena-frontend-coral.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "HelenaApp",
        ValidAudience = "HelenaApp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hardCodedKeyForNow1234567890!_testeeeee"))
    };
});

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Definir o esquema de seguran�a
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira 'Bearer' [espa�o] e o token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
                new string[] {}
            }
        });
});
var connectionString = GetConnectionString(builder.Configuration);

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string is not configured. Set ConnectionStrings__DefaultConnection, DATABASE_URL, or provide PostgreSQL env vars (PGHOST/PGUSER/PGPASSWORD/PGDATABASE).");
}

// Convert PostgreSQL URI format to Npgsql connection string format if needed.
if (connectionString.StartsWith("postgresql://") || connectionString.StartsWith("postgres://"))
{
    var uri = new Uri(connectionString);
    var builder_cs = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port == -1 ? 5432 : uri.Port,
        Username = uri.UserInfo.Split(':')[0],
        Password = uri.UserInfo.Split(':')[1],
        Database = uri.LocalPath.TrimStart('/'),
        SslMode = SslMode.Prefer,
        TrustServerCertificate = true
    };
    connectionString = builder_cs.ConnectionString;
}

builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infra.Data"));
});

static string? GetConnectionString(IConfiguration config)
{
    var raw = config.GetConnectionString("DefaultConnection")
        ?? config["ConnectionStrings:DefaultConnection"]
        ?? config["ConnectionStrings__DefaultConnection"]
        ?? config["DATABASE_URL"];

    if (!string.IsNullOrWhiteSpace(raw) && raw.Contains("${{"))
    {
        raw = null;
    }

    if (!string.IsNullOrWhiteSpace(raw))
    {
        return raw;
    }

    var host = config["PGHOST"] ?? config["RAILWAY_PRIVATE_DOMAIN"];
    var user = config["PGUSER"] ?? config["POSTGRES_USER"];
    var password = config["PGPASSWORD"] ?? config["POSTGRES_PASSWORD"];
    var database = config["PGDATABASE"] ?? config["POSTGRES_DB"];
    var portString = config["PGPORT"] ?? "5432";

    if (string.IsNullOrWhiteSpace(host)
        || string.IsNullOrWhiteSpace(user)
        || string.IsNullOrWhiteSpace(password)
        || string.IsNullOrWhiteSpace(database))
    {
        return null;
    }

    var port = int.TryParse(portString, out var p) ? p : 5432;
    var builder_cs = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = port,
        Username = user,
        Password = password,
        Database = database,
        SslMode = SslMode.Prefer,
        TrustServerCertificate = true
    };

    return builder_cs.ConnectionString;
}

builder.Services.AddScoped<IAppUserBusiness, AppUserBusiness>();
builder.Services.AddScoped<IAppUserData, AppUserData>();
builder.Services.AddScoped<IMedicationBusiness, MedicationBusiness>();
builder.Services.AddScoped<ITimesBusiness, TimesBusiness>();
builder.Services.AddScoped<IMedicationData, MedicationData>();
builder.Services.AddScoped<ITimesData, TimesData>();    
builder.Services.AddScoped<IDoctorData, DoctorData>();


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllers();

await app.RunAsync();
