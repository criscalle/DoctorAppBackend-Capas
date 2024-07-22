using API.Extenciones;
using Data;
using Data.Interfaces;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServiceApplication(builder.Configuration); // inyecta ServiceApplicationExtension
builder.Services.AddServiceIdentity(builder.Configuration); // inyecta ServiceIdentityExtension
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());

app.UseAuthentication(); // se pone para utilizar la autenticacion que se inyectó con el AddAuthentication y se configuró en el swagger en AddSwaggerGen
app.UseAuthorization();

app.MapControllers();

app.Run();
