using Data.Interfaces;
using Data.Services;
using Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errores;
using Data.Repository;
using Data.Interfaces.IRepository;
using Utilities;
using BLL.Services.Interfaces;
using BLL.Services;

namespace API.Extenciones;

public static class ServiceApplicationExtension
{
    public static IServiceCollection AddServiceApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {                                  // se le agrega al addswagger todo lo que tiene para que nos permita la autorizacion en la api
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Ingresar Bearer [espacio] token \r\n\r\n " +
                            "Ejemplo: Bearer ejoy887788999909000",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",

                },
                Scheme  = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
        });
        var connectionString = config.GetConnectionString("DefaultConnection"); // se inyecta la conexion a DB

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddCors(); // el cors se pone para que la api permita conexion desde otros programas (Angular) pero se termina de configurar abajo en app.usercors

        services.AddScoped<ITokenServices, TokenService>(); // se inyecta el token y se hace con AddScoped por que se necesita un servicio por solicitud (que se desechen una vez utilizados)

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var errores = ActionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errores = errores
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });
        services.AddScoped<IUnitWork, UnitWork>(); // aqui se inserta la unidad de trabajo y el servicio ya esta inyectado en el program
        services.AddAutoMapper(typeof(MappingProfile)); // aqui se inserta el automapper pero se debe agregar referencia de proyecto
        services.AddScoped<ISpecialityService, SpecialityService>();
        services.AddScoped<IMedicoService, MedicoService>();

        return services;  // este es un metodo de extension. todo lo de adentro estaba en el program inyectado con builder.Service pero se organiza con el metodo de extencion y se reemplaza el builder.Service por el service del parametro y el config tambien del parametro y se agrega asi: (builder.Services.AddServiceApplication(builder.Configuration))
    }
}
