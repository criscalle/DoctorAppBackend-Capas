using API.Extenciones;
using API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServiceApplication(builder.Configuration); // inyecta ServiceApplicationExtension en extenciones
builder.Services.AddServiceIdentity(builder.Configuration); // inyecta ServiceIdentityExtension en extenciones
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

app.MapControllers();

app.Run();
