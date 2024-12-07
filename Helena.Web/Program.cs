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
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Infra.Data"));
});

builder.Services.AddScoped<IAppUserBusiness, AppUserBusiness>();
builder.Services.AddScoped<IAppUserData, AppUserData>();
builder.Services.AddScoped<IMedicationBusiness, MedicationBusiness>();
builder.Services.AddScoped<ITimesBusiness, TimesBusiness>();


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllers();

app.Run();
