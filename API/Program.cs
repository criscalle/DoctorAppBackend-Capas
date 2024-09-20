using API.Extenciones;
using API.Middleware;
using Data.Inicializador;
using Microsoft.OpenApi.Writers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServiceApplication(builder.Configuration); // inyecta ServiceApplicationExtension en extenciones
builder.Services.AddServiceIdentity(builder.Configuration); // inyecta ServiceIdentityExtension en extenciones

builder.Services.AddScoped<IdbInicializador, DbInicializador>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); // se inyecta el ExceptionMiddleware que son errores del servidor
app.UseStatusCodePagesWithReExecute("/errores/{0}");

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

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerfactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {
        var logger = loggerfactory.CreateLogger<Program>();
        logger.LogError(ex, "Un Error ocurrió al ejecutar la migración");
    }
}

app.MapControllers();

app.Run();
